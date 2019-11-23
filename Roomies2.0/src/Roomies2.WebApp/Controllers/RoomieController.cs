using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models.AccountViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
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
            Result<RoomieData> result = await _roomieGateway.FindById(roomieId);
            return this.CreateResult(result);
        }

        [HttpGet("getRoomieByEmail/{email}")]
        public async Task<IActionResult> GetRoomieByEmail(string email)
        {
            Result<RoomieData> result = await _roomieGateway.FindRoomieByEmail(email);
            return this.CreateResult(result);
        }

        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail()
        {
            string email = (HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email).Value);

            Result<UserData> result = await _roomieGateway.FindUserByEmail(email);
            return this.CreateResult(result);
        }


        [HttpGet("profile", Name ="Profile")]
        public async Task<IActionResult> GetProfile()
        {
            int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            // Result<RoomieProfile> result = await _roomieGateway.GetProfile(roomieId);
           
            Result<RoomieData> roomie = await _roomieGateway.FindById(roomieId);

            return this.CreateResult(roomie);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomieViewModel model)
        {
            int id = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            string picpath = "";

            Result<int> result = await _roomieGateway.Create( id, model.LastName, model.FirstName, model.Phone, model.Sex, model.BirthDate, model.Description, picpath);
            return this.CreateResult(result);
        }
    }
}
