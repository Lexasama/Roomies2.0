#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.Grocery;
using Roomies2.WebApp.Authentication;

#endregion

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class GroceryController : Controller
    {
        public GroceryController(GroceriesGateway gateway) => Gateway = gateway;

        private GroceriesGateway Gateway { get; }

        [HttpGet(Name = "GetAllList")]
        public async Task<List<GroceryList>> GetAllList(int colocId) =>
            await Gateway.GetAllGroceryListFromColocId(colocId);

        [HttpGet("{GroceryListId}", Name = "GetGroceryListById")]
        public async Task<GroceryList> GetGroceryListById(int groceryListId) =>
            await Gateway.GetGroceryListById(groceryListId);

        [HttpPost(Name = "CreateGroceryList")]
        public async Task<IActionResult> CreateGroceryList(
            [FromBody] int colocId, [FromBody] int roomieId,
            [FromBody] string name, [FromBody] DateTime dueDate) =>
            this.CreateResult(await Gateway.CreateGroceryList(colocId, roomieId, name, dueDate));

        [HttpDelete("{ListId}", Name = "DeleteGroceryList")]
        public async Task<IActionResult> DeleteGroceryList(int listId) =>
            this.CreateResult(await Gateway.DeleteGroceryList(listId));

        [HttpPost(Name = "UpdateGroceryList")]
        public async Task<IActionResult> UpdateGroceryList(
            [FromBody] int groceryListId,
            [FromBody] int roomieId,
            [FromBody] string name,
            [FromBody] DateTime dueDate) =>
            this.CreateResult(await Gateway.UpdateGroceryList(groceryListId, roomieId, name, dueDate));

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