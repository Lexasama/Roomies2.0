using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ColocController : Controller
    {
        private readonly ColocGateway _colocGateway;

        public ColocController(ColocGateway colocGateway)
        {
            _colocGateway = colocGateway;
        }

        [HttpDelete("{colocId}", Name = "GetColoc")]
        public async Task<IActionResult> GetColoc(int colocId)
        {
            var result = await _colocGateway.FindById(colocId);
            return this.CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColoc([FromBody] ColocViewModel model)
        {
            //string picPath = _path + "ColocPics" +  ;
            var picPath = "";
            var result = await _colocGateway.Create(model.RoomieId, model.ColocName, picPath);
            return this.CreateResult(result);
        }
    }
}