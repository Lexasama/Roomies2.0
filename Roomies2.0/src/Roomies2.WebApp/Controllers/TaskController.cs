using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using Roomies2.WebApp.Authentication;
using Roomies2.WebApp.Models;
using System.Collections.Generic;
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

        [HttpGet("{taskId}", Name = "getTask")]
        public async Task<IActionResult> GetTask(int taskId)
        {
            var result = await _tasksGateway.Find(taskId);
            return this.CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskViewModel model)
        {

            Result<int> result = await _tasksGateway.Create(model.TaskName, model.TaskDes, model.TaskDate, model.ColocId);
            int taskId = result.Content;
            if (result.Status == Status.Created)
            {
                foreach (int roomieId in model.Roomies)
                {
                    await _tasksGateway.Assign(taskId, roomieId);
                }
            }
            return this.CreateResult(result, o =>
            {
                o.RouteName = "getTask";
                o.RouteValues = taskId => new { taskId };
            });
        }

        [HttpGet("getTasks/{colocId}")]
        public async Task<IActionResult> GetTaskList(int colocId)
        {
            List<TaskSuper> tasks = new List<TaskSuper>();

            IEnumerable<TaskData> taskList = await _tasksGateway.GetTasks(colocId);
            foreach (TaskData task in taskList)
            {
                var r = await _tasksGateway.GetAssignedRoomies(task.TaskId);
                TaskSuper ts = new TaskSuper(task, r);
                tasks.Add(ts);
            }

            return Ok(tasks);
        }

        [HttpGet("getActiveTasks/{colocId}/{isActive}")]
        public async Task<IActionResult> getColocActiveTask(int colocId, bool isActive)
        {
            List<TaskSuper> tasks = new List<TaskSuper>();

            IEnumerable<TaskData> taskList = await _tasksGateway.GetTasks(colocId, isActive);
            foreach (TaskData task in taskList)
            {
                var r = await _tasksGateway.GetAssignedRoomies(task.TaskId);
                TaskSuper ts = new TaskSuper(task, r);
                tasks.Add(ts);
            }

            return Ok(tasks);
        }

        [HttpPost("updateState/{taskId}")]
        public async Task<IActionResult> UpdateTaskState(int taskId)
        {
            Result result = await _tasksGateway.UpdateState(taskId);
            return this.CreateResult(result);
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            Result result = await _tasksGateway.Delete(taskId);
            return this.CreateResult(result);
        }
    }
}
