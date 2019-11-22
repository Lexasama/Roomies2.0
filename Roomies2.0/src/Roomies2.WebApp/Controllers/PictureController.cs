using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Roomies2.DAL.Gateways;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Roomies2.WebApp.Authentication;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class PictureController : Controller
    {
        readonly PictureGateway _pictureGateway;

        public PictureController(PictureGateway pictureGateway)
        {
            _pictureGateway = pictureGateway;
        }
        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormCollection model)
        {

            bool isRoomie = bool.Parse((model.ToList().Find(x => x.Key == "isRoomie").Value.ToString()));

            int id = int.Parse((model.ToList().Find(x => x.Key == "id").Value.ToString()));

            await _pictureGateway.UploadPicture(model.Files[0], id, isRoomie);
           
            return Ok();

        }
    }
}
