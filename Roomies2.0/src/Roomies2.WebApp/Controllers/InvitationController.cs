using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;
using Roomies2.WebApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class InvitationController: Controller
    {
        public InviteGateway _invitationGateway;
        public EmailSender _emailSender = new EmailSender(@"D:\invite.html");                                 


        public InvitationController( InviteGateway invitationGateway)
        {
            _invitationGateway = invitationGateway;
            
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]

        public async Task<IActionResult> Invite([FromBody] InviteViewModel model)
        {
            int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            Result result = null;
            foreach(string email in model.Emails)
            {
                result = await _invitationGateway.Invite(roomieId, model.ColocId, email);

                if (result.Status == Status.Ok )
                {
                    _emailSender.SendEmail(email);

                }

                return this.CreateResult(result);

            }
            return this.CreateResult(result);

        }

        //[HttpPost("roomie/invite/{code}")]
        //public async Task<IActionResult> Invited(string code)
        //{
        //}

    }
}
