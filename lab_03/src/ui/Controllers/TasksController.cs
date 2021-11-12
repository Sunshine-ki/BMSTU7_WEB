using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ui.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

using bl;
using Head;
using ui.Helpers;
using ui.dto;
using Microsoft.AspNetCore.Authorization;

namespace ui.Controllers
{
	public class TasksController : Controller
	{
		Head.Services.TaskService _taskService;
		private readonly ILogger<TasksController> _logger;

		public TasksController(ILogger<TasksController> logger, Head.Services.TaskService taskService)
		{
			_logger = logger;
			_taskService = taskService;
		}

		//////
		[HttpGet("tasks")] 
		public string Index()
		{
			var tasks = Converter.ConvertTasksToUI(_taskService.GetTasks());
			tasks.ForEach(task => task.Solution = "");
			string jsonString = JsonSerializer.Serialize(tasks, Options.JsonOptions());
			return jsonString;
		}

		[HttpGet("task{task_id}")] 
  		public string Task([FromRoute] int task_id)
		{
			var task = _taskService.GetTask(task_id);
			task.Solution = "";
			string jsonString = JsonSerializer.Serialize(task, Options.JsonOptions());
			return jsonString;
		}

		
		[HttpPost("task{task_id}")]
		public string Task([FromRoute] int task_id, [FromBody] ui.Models.Task userTask)
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
			{
				return JsonSerializer.Serialize(new ResultDTO() {Title = "Not authorized"}, Options.JsonOptions());
			}
			
			var userSolution = userTask.Solution;

			bl.Task taskBL = _taskService.GetTask(task_id);
			if (taskBL is null)
			{
				// https://httpstatuses.com/
		        this.HttpContext.Response.StatusCode = 418; 
				return JsonSerializer.Serialize(new ResultDTO() {Title = "task is not exists"}, Options.JsonOptions());
			}

			var result = _taskService.CompareSolution(userSolution, task_id); 
			
			if (result.returnValue == Head.Constants.OK)
			{
				return JsonSerializer.Serialize(new ResultDTO() {Title = "Well done"}, Options.JsonOptions());
			}

			return JsonSerializer.Serialize(new ResultDTO() {Title = result.Msg}, Options.JsonOptions());
		}
	}
}
