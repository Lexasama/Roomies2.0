using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Roomies2.DAL.Gateways;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models.AccountViewModels;
using Roomies2.WebApp.Services;

namespace Roomies2.WebApp.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(UserGateway userGateway, UserService userService, TokenService tokenService,
            IAuthenticationSchemeProvider authenticationSchemeProvider, IOptions<SpaOptions> spaOptions)
        {
            UserGateway = userGateway;
            UserService = userService;
            TokenService = tokenService;
            AuthenticationSchemeProvider = authenticationSchemeProvider;
            SpaOptions = spaOptions;
            Random = new Random();
        }

        public UserGateway UserGateway { get; }
        public UserService UserService { get; }
        public TokenService TokenService { get; }
        public IAuthenticationSchemeProvider AuthenticationSchemeProvider { get; }
        public Random Random { get; }
        public IOptions<SpaOptions> SpaOptions { get; }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await UserService.FindUser(model.Email, model.Password);
                if (account == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                await SignIn(account.Email, account.UserId.ToString());
                return RedirectToAction(nameof(Authenticated));
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserService.CreatePasswordUser(model.UserName, model.Email, model.LastName,
                    model.FirstName, model.Phone, model.Sex, model.BirthDate, model.Password);
                if (result.HasError)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(model);
                }

                await SignIn(model.Email, result.Content.ToString());
                return RedirectToAction(nameof(Authenticated));
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthentication.AuthenticationScheme);
            ViewData["SpaHost"] = SpaOptions.Value.Host;
            ViewData["NoLayout"] = true;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin([FromQuery] string provider)
        {
            // Note: the "provider" parameter corresponds to the external
            // authentication provider choosen by the user agent.
            if (string.IsNullOrWhiteSpace(provider)) return BadRequest();

            if (await AuthenticationSchemeProvider.GetSchemeAsync(provider) == null) return BadRequest();

            // Instruct the middleware corresponding to the requested external identity
            // provider to redirect the user agent to its own authorization endpoint.
            // Note: the authenticationScheme parameter must match the value configured in Startup.cs
            string redirectUri = Url.Action(nameof(ExternalLoginCallback), "Account");
            return Challenge(new AuthenticationProperties {RedirectUri = redirectUri}, provider);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public IActionResult ExternalLoginCallback()
        {
            return RedirectToAction(nameof(Authenticated));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public async Task<IActionResult> Authenticated()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string email = User.FindFirst(ClaimTypes.Email).Value;
            var token = TokenService.GenerateToken(userId, email);
            var providers = await UserGateway.GetAuthenticationProviders(userId);
            ViewData["SpaHost"] = SpaOptions.Value.Host;
            ViewData["BreachPadding"] = GetBreachPadding(); // Mitigate BREACH attack. See http://www.breachattack.com/
            ViewData["Token"] = token;
            ViewData["Email"] = email;
            ViewData["NoLayout"] = true;
            ViewData["Providers"] = providers;
            return View();
        }

        private async Task SignIn(string email, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email, ClaimValueTypes.String),
                new Claim(ClaimTypes.NameIdentifier, userId, ClaimValueTypes.String)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthentication.AuthenticationType, ClaimTypes.Email,
                string.Empty);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthentication.AuthenticationScheme, principal);
        }

        private string GetBreachPadding()
        {
            var data = new byte[Random.Next(64, 256)];
            Random.NextBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}