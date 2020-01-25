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
        public RoomieGateway Gateway { get; }

        public RoomieController(RoomieGateway roomieGateway) => Gateway = roomieGateway;

        [HttpGet("{roomieId}", Name = "getRoomie")]
        public async Task<IActionResult> GetRoomie(int id)
        {
            if(id <= 0) id = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = await Gateway.FindById(id);
            return this.CreateResult(result);
        }

        [HttpGet("getRoomieByEmail/{email}")]
        public async Task<IActionResult> GetRoomieByEmail(string email)
        {
            if (email == "undefined") email = (HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email).Value);
            var result = await Gateway.FindRoomieByEmail(email);
            return this.CreateResult(result);
        }

        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail()
        {
            string email = (HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email).Value);

            var result = await Gateway.FindUserByEmail(email);
            return this.CreateResult(result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
           int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
           var roomie = await Gateway.GetProfile(roomieId);

            return this.CreateResult(roomie);
        }
        [HttpGet("profile/{roomieId}")]
        public async Task<IActionResult> GetProfile(int roomieId)
        {
            if(roomieId == 0 ) roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = await Gateway.GetProfile(roomieId);
            return this.CreateResult(result);
        }


        [HttpGet("getRoomies/{colocId}")]
        public async Task<IActionResult> GetRoomies(int colocId)
        {

            var roomies = await Gateway.GetRoomies(colocId);
            return this.CreateResult(roomies);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomieViewModel model)
        {
            int id = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var result = await Gateway.Create( id, model.UserName, model.LastName, model.FirstName, model.Phone, model.Sex, model.BirthDate, model.Description, model.PicturePath);
            return this.CreateResult(result, o=>
            {
                o.RouteName = "getRoomie";
                o.RouteValues = roomieId => new { roomieId };
            });
        }

        [HttpGet("picture/{roomieId}")]
        public async Task<IActionResult> GetPicture(int roomieId)
        {
            if (roomieId == 0) roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var result = await Gateway.GetPicture(roomieId);
            return this.CreateResult(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoomieViewModel model)
        {
            int roomieId = int.Parse(HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            Result result = await Gateway.Update(roomieId,model.UserName,  model.Email, model.LastName, model.FirstName, model.Phone, model.Sex, model.BirthDate, model.Description, model.PicturePath);
           return this.CreateResult(result);
        }
    }
}