using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;
using Roomies2.WebApp.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Controllers
{
    [Route("/api/[controller]")]
    public class InvitationController: Controller
    {
        public InviteGateway _invitationGateway;
        public ColocGateway _colocGateway;
        public EmailSender _emailSender = new EmailSender(@"./wwwroot/assets/invite/Emails/InviteEmail.html");                                 


        public InvitationController( InviteGateway invitationGateway, ColocGateway colocGateway)
        {
            _invitationGateway = invitationGateway;
            _colocGateway = colocGateway;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
        public async Task<IActionResult> Invite([FromBody] InviteViewModel model)
        {
            int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            Result result = null;
            foreach(string email in model.Emails)
            {
                string code = Guid.NewGuid().ToString().Replace(" ", "9").Substring(0, 12);
                result = await _invitationGateway.Invite(roomieId, model.ColocId, email, code);
                if (result.Status == Status.Ok )
                {
                    _emailSender.SendEmail(email, code);

                }

                return this.CreateResult(result);

            }
            return this.CreateResult(result);

        }

        [HttpPost("join/{code}")]
        public async void Invite(string code)
        {
            var result = await _invitationGateway.FindInvite(code);
            
            if(result.Status == Status.Ok)
            {
                var invitation = result.Content;
                await _colocGateway.AddToColoc(invitation.ColocId, invitation.Email);
                await _colocGateway.DeleteInvite(code);
            }

        }
    }
}
