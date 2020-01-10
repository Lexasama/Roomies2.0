using System;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Roomies2.DAL.Gateways;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Controllers;
using Roomies2.WebApp.Services;

namespace Roomies2.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOptions();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddSingleton(_ => new UserGateway(Configuration["ConnectionStrings:Roomies2DB"]));
            services.AddSingleton(_ => new PictureGateway(Configuration["ConnectionStrings:Roomies2DB"]));
            services.AddSingleton(_ => new RoomieGateway(Configuration["ConnectionStrings:Roomies2DB"]));
            services.AddSingleton(_ => new ColocGateway(Configuration["ConnectionStrings:Roomies2DB"]));
            services.AddSingleton(_ => new TasksGateway(Configuration["ConnectionStrings:Roomies2DB"]));
            services.AddSingleton(_ => new InviteGateway(Configuration["ConnectionStrings:Roomies2DB"]));
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<UserService>();
            services.AddSingleton<TokenService>();
            services.AddSingleton<GoogleAuthenticationManager>();
            services.AddSingleton<FacebookAuthenticationManager>();

            string secretKey = Configuration["JwtBearer:SigningKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));


            services.Configure<TokenProviderOptions>(o =>
            {
                o.Audience = Configuration["JwtBearer:Audience"];
                o.Issuer = Configuration["JwtBearer:Issuer"];
                o.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.Configure<SpaOptions>(o =>
            {
                o.Host = Configuration["Spa:Host"];
            });

            services.AddAuthentication(CookieAuthentication.AuthenticationScheme)
                .AddCookie(CookieAuthentication.AuthenticationScheme, o =>
                {
                    o.ExpireTimeSpan = TimeSpan.FromHours(1);
                    o.SlidingExpiration = true;
                })
                .AddJwtBearer(JwtBearerAuthentication.AuthenticationScheme, o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,

                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtBearer:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtBearer:Audience"],

                        NameClaimType = ClaimTypes.Email,
                        AuthenticationType = JwtBearerAuthentication.AuthenticationType,

                        ValidateLifetime = true
                    };
                })
                .AddGoogle(o =>
                {
                    o.SignInScheme = CookieAuthentication.AuthenticationScheme;
                    o.ClientId = Configuration["Authentication:Google:ClientId"];
                    o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    //o.Scope.Add("https://www.googleapis.com/auth/user.birthday.read");

                    o.Events = new OAuthEvents
                    {
                        OnCreatingTicket = ctx => ctx.HttpContext.RequestServices.GetRequiredService<GoogleAuthenticationManager>().OnCreatingTicket(ctx)
                    };

                    o.AccessType = "offline";
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.SignInScheme = CookieAuthentication.AuthenticationScheme;

                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    facebookOptions.Events = new OAuthEvents
                    {
                        OnCreatingTicket = ctx => ctx.HttpContext.RequestServices.GetRequiredService<FacebookAuthenticationManager>().OnCreatingTicket(ctx)
                    };

                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowCredentials();
                c.WithOrigins(Configuration["Spa:Host"]);
            });

            string secretKey = Configuration["JwtBearer:SigningKey"];
            SymmetricSecurityKey unused = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Account", action = "Login" });
            });

            app.UseStaticFiles();
        }
    }
}
