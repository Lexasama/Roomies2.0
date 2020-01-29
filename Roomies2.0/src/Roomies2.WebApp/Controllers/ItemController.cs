using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.Grocery;
using Roomies2.WebApp.Authentication;

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

        [HttpPost(Name = "AddItem")]
        public async Task<IActionResult> AddItem([FromBody] string itemName, [FromBody] int unitPrice) =>
            this.CreateResult(await Gateway.AddItem(itemName, unitPrice));

        [HttpDelete("{itemId}", Name = "DeleteItem")]
        public async Task<int> DeleteItem(int itemId) => await Gateway.DeleteItem(itemId);
    }
}