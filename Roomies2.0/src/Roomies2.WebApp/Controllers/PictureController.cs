using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class PictureController : Controller
    {
        [HttpPost("uploadColoc/{id}/{isRoomie}")]
        public async Task<IActionResult> UploadImage(IFormCollection model, int id, bool isRoomie)
        {
            if (isRoomie)
            {
                //id = int.Parse( model.ToList().Find( x => x.Key == "roomieId" ).Value.ToString() );
                id = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
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
