using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Roomies2.WebApp.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Roomies2.DAL.Gateways;
using System.Collections.Generic;
using System.Linq;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class PictureController : Controller
    {
        readonly PictureGateway _pictureGateway;

        public PictureController(PictureGateway pictureGateway)
        {
            _pictureGateway = pictureGateway;
        }
        [HttpPost("uploadColoc")]
        public async Task<IActionResult> UploadImage(IFormCollection model)
        {
           // int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);


            bool isRoomie = bool.Parse((model.ToList().Find(x => x.Key == "isRoomie").Value.ToString()));


            int x = int.Parse((model.ToList().Find(x => x.Key == "id").Value.ToString()));

            await _pictureGateway.UploadPicture(model.Files[0], x, isRoomie);
           
            return Ok();

        }
    }
}
