using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    //[Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class RoomieController : Controller
    {

        readonly RoomieGateway _roomieGateway;

        public RoomieController(RoomieGateway roomieGateway)
        {
            _roomieGateway = roomieGateway;
        }
  
        [HttpGet("{roomieId}", Name = "GetRoomie")]
        public async Task<IActionResult> GetRoomie(int roomieId)
        {
            string id = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Result<RoomieData> result = await _roomieGateway.FindById(roomieId);
            return this.CreateResult(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRoomie([FromBody] RoomieViewModel model)
        //{
        //    int id = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //    Result<int> result = await _roomieGateway.Create(model.Name, model.Level);
        //    return this.CreateResult(result);
        //}
    }
}
