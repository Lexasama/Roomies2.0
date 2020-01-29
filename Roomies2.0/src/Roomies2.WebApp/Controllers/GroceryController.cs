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
        public async Task<List<GroceryList>> GetAllList(int colocId) =>
            await Gateway.GetAllGroceryListFromColocId(colocId);

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

        [HttpPost("UpdateGroceryList")]
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

        [HttpGet("{groceryListId}", Name = "GetAllItemsInGroceryList")]
        public async Task<List<Item>> GetAllItemsInGroceryList(int groceryListId) =>
            await Gateway.GetAllItemsFromGroceryListId(groceryListId);

        [HttpGet(Name = "AddUpdateOrDeleteItemsInGroceryList")]
        public async Task AddUpdateOrDeleteItemsInGroceryList(
            [FromBody] int groceryListId, [FromBody] int itemId, [FromBody] int amount) =>
            this.CreateResult(
                await Gateway.AddUpdateOrDeleteItemToGroceryList(groceryListId, itemId, amount));
    }
}