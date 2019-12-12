using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ColocController : Controller
    {
        public ColocGateway Gateway { get; }

        public ColocController(ColocGateway colocGateway)
        {
            Gateway = colocGateway;

        }

        [HttpGet("{colocId}", Name = "GetColoc")]
        public async Task<IActionResult> GetColoc(int colocId)
        {
            var result = await Gateway.FindById(colocId);
            return this.CreateResult(result);
        }
        [HttpGet("colocList/{roomieId}")]
        public async Task<IActionResult> GetColocList(int roomieId)
        {
            if (roomieId <= 0) roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var colocList = await Gateway.GetList(roomieId);
            return this.CreateResult(colocList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColoc([FromBody] ColocViewModel model)
        {
            int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var result = await Gateway.Create(roomieId, model.ColocName);
            return this.CreateResult(result, o =>
            {
                o.RouteName = "GetColoc";
                o.RouteValues = colocId => new { colocId };
            }) ;

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ColocViewModel model)
        {
            Result result = await Gateway.Update(model.ColocId, model.ColocName, model.PicPath);
            return this.CreateResult(result);
        }
    }
}