using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class RoomieController : Controller
    {
        public RoomieController(RoomieGateway roomieGateway)
        {
            Gateway = roomieGateway;
        }

        private RoomieGateway Gateway { get; }

        [HttpGet("{roomieId}", Name = "GetRoomie")]
        public async Task<IActionResult> GetRoomie(int roomieId)
        {
            var result = await Gateway.FindById(roomieId);
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