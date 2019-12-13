using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Controllers
{
    [Route("api/[controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class TaskController : Controller
    {
        public TasksGateway _tasksGateway;

        public TaskController(TasksGateway taskController)
        {
            _tasksGateway = taskController;
        }

        [HttpPost]
        public async Task<  IActionResult> Create([FromBody] TaskViewModel model)
        {

            var result = await _tasksGateway.Create(model.TaskName, model.TaskDes, model.TaskDate, model.ColocId)
                
                
                //Test task creation and get taskId before assignement

            foreach(int roomieId in model.Roomies)
            {

            }
        }

    }
}
