#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.Grocery;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;

#endregion

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class GroceryController : Controller
    {
        public GroceryController(GroceriesGateway gateway) => Gateway = gateway;

        private GroceriesGateway Gateway { get; }

        [HttpGet("GetAllList/{colocId}")]
        public async Task<IActionResult> GetAllList(int colocId)
        {
            var result = await Gateway.GetAllGroceryListFromColocId(colocId);
            return this.CreateResult(result);
        }
        [HttpGet("{GroceryListId}", Name = "GetGroceryList")]
        public async Task<Result<GroceryList>> GetGroceryListById(int groceryListId) =>
            await Gateway.GetGroceryListById(groceryListId);

        [HttpPost]
        public async Task<IActionResult> CreateGroceryList([FromBody] GroceryListModel model)
        {
            var result = await Gateway.CreateGroceryList( model.ColocId,model.RoomieId,model.Name, model.DueDate
                );
            return this.CreateResult( result, o =>
            {
                o.RouteName = "GetGroceryList";
                o.RouteValues = groceryListId => new { groceryListId };
            });
        }

        [HttpDelete("{listId}")]
        public async Task<IActionResult> DeleteGroceryList(int listId) =>
            this.CreateResult(await Gateway.DeleteGroceryList(listId));

        [HttpPut("UpdateGroceryList")]
        public async Task<IActionResult> UpdateGroceryList(
            [FromBody] GroceryListModel model) =>
            this.CreateResult(
                await Gateway.UpdateGroceryList(
                    model.GroceryListId,
                    model.RoomieId,
                    model.Name,
                    model.DueDate
                    )
                );

        [HttpGet("getItems/{groceryListId}")]
        public async Task<IActionResult> GetAllItemsInGroceryList(int groceryListId)
        {
            var result = await Gateway.GetAllItemsFromGroceryListId(groceryListId);
         return this.CreateResult(result);
        }

        [HttpPost("AddToList")]
        public async Task<IActionResult> AddItemToList([FromBody] GroceryListItemsViewModel model)
        {
            Result result = null;

            foreach(int id in model.ItemIdList)
            {
               result =  await Gateway.AddItemToList(id, model.GroceryListId);
            }

            return this.CreateResult(result);
        
        }
    }
}