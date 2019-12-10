using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models.AccountViewModels;
using System;
using System.Collections.Generic;
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
  
        [HttpGet("{id}", Name = "GetRoomie")]
        public async Task<IActionResult> GetRoomie(int id)
        {
            if(id == 0) id = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Result<RoomieData> result = await _roomieGateway.FindById(id);
            return this.CreateResult(result);
        }

        [HttpGet("getRoomieByEmail/{email}")]
        public async Task<IActionResult> GetRoomieByEmail(string email)
        {
            if (email == "undefined") email = (HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email).Value);
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

        [HttpGet("user")]
        public async Task<IActionResult> getUser()
        {
           int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
           Result<RoomieProfile> roomie = await _roomieGateway.GetProfile(roomieId);

            return this.CreateResult(roomie);
        }
        [HttpGet("profile/{roomieId}")]
        public async Task<IActionResult> GetProfile(int roomieId)
        {
            if(roomieId == 0 ) roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Result<RoomieProfile> result = await _roomieGateway.GetProfile(roomieId);
            return this.CreateResult(result);
        }


        [HttpGet("getRoomies/{colocId}")]
        public async Task<IActionResult> getRoomies(int colocId)
        {

            Result<IEnumerable<RoomieProfile>> roomies = await _roomieGateway.GetRoomies(colocId);
            return this.CreateResult(roomies);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomieViewModel model)
        {
            int id = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            Result<int> result = await _roomieGateway.Create( id, model.UserName, model.LastName, model.FirstName, model.Phone, model.Sex, model.BirthDate, model.Description, model.PicturePath);
            return this.CreateResult(result, o=>
            {
                o.RouteName = "GetRoomie";
                o.RouteValues = id => new { id };
            });
        }

        [HttpGet("picture/{roomieId}")]
        public async Task<IActionResult> GetPicture(int roomieId)
        {
            if (roomieId == 0) roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            Result<string> result = await _roomieGateway.GetPicture(roomieId);
            return this.CreateResult(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoomieViewModel model)
        {
            int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            Result result = await _roomieGateway.Update(roomieId,model.UserName,  model.Email, model.LastName, model.FirstName, model.Phone, model.Sex, model.BirthDate, model.Description, model.PicturePath);
           return this.CreateResult(result);
        }
    }
}
