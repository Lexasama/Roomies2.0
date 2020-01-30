using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.Grocery;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ItemController : Controller
    {
        public ItemController(ItemGateway gateway) => Gateway = gateway;
        public ItemGateway Gateway { get; set; }

        [HttpGet("GetAllItems")]
        public async Task<List<Item>> GetAllItems() => await Gateway.GetAllItems();

        [HttpGet("{itemId}", Name="getItem")]
        public async Task<IActionResult> GetItem(int itemId)
        {
            var result = await Gateway.FindItemById(itemId);
            return this.CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] ItemViewModel model)
        {
            var result = await Gateway.AddItem(model.ItemName, model.UnitPrice);
           return  this.CreateResult(result, o =>
           {
               o.RouteName = "getItem";
               o.RouteValues = itemId => new { itemId };
           }    );

        }

        [HttpDelete("{itemId}", Name = "DeleteItem")]
        public async Task<int> DeleteItem(int itemId) => await Gateway.DeleteItem(itemId);
    
        [HttpPost("increase/{itemId}")]
        public async Task<IActionResult> Increase(int itemId)
        {
            var result = await Gateway.IncreaseQuantity(itemId);

            return this.CreateResult(result);
        }

        [HttpPost("decrease/{itemId}")]
        public async Task<IActionResult> DecreaseQuantity(int itemId)
        {
            var result = await Gateway.DecreaseQuantity(itemId);

            return this.CreateResult(result);
        }
    }
}