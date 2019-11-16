using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ColocController : Controller
    {
        readonly ColocGateway _colocGateway;
        public ColocController(ColocGateway colocGateway)
        {
            _colocGateway = colocGateway;

        }

        [HttpDelete("{colocId}", Name = "GetColoc")]
        public async Task<IActionResult> GetColoc(int colocId)
        {
            Result<ColocData> result = await _colocGateway.FindById(colocId);
            return this.CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColoc([FromBody] ColocViewModel model)
        {
            //string picPath = _path + "ColocPics" +  ;
            string picPath = "";
            Result<int> result = await _colocGateway.Create(model.RoomieId, model.ColocName, picPath);
            return this.CreateResult(result);

        }
    }
}
