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

		[HttpGet("task_old")]
		public IActionResult task(int taskId)
		{
			// if (string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
			// 	return Redirect("/Home/Registration");

			bl.Task taskBL = _taskService.GetTask(taskId);
			if (taskBL is null)
			{
				return Redirect("/tasks");
			}
			ui.Models.Task task = Converter.ConvertTaskToUI(taskBL);
			ViewBag.task = task;
			ViewBag.info_text = "Решите задачу";
			ViewBag.colors = "alert alert-success";
			return View();
		}

		[HttpPost("task_old")]
		public IActionResult TaskOld(string userSolution, int taskId)
		{
			Console.WriteLine($"user_solution = {userSolution} TaskId = {taskId}");
			
			bl.Task taskBL = _taskService.GetTask(taskId);
			if (taskBL is null)
			{
				return Redirect("/Tasks");
			}

			ui.Models.Task task = Converter.ConvertTaskToUI(taskBL);
			ViewBag.task = task;

			var result = _taskService.CompareSolution(userSolution, taskId); 
			
			if (result.returnValue == Head.Constants.OK)
			{
				ViewBag.info_text = "Задача решена!";
				ViewBag.colors = "alert alert-success";
			}
			else 
			{
				ViewBag.info_text = result.Msg;
				ViewBag.colors = "alert alert-danger";
			}

			return View();
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
			var userSolution = userTask.Solution;

			bl.Task taskBL = _taskService.GetTask(task_id);
			if (taskBL is null)
			{
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
