using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Roomies2.WebApp.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Roomies2.DAL.Gateways;

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
        [HttpPost("uploadColoc/{id}/{isRoomie}")]
        public async Task<IActionResult> UploadImage(IFormCollection model, int id, bool isRoomie)
        {
            //int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (isRoomie)
            {
              

                await _pictureGateway.UploadPicture(model.Files[0], id, isRoomie);

            }
           
            //List<string> result = await _imageGateway.UploadImage(model.Files, id, isRoomie);
            //if (result.Count == 0)
            //{
            //    return Ok();
            //}
            //return Ok(result);
            return Ok();

        }
    }
}
