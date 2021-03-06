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
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

using bl;
using Head;
using ui.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ui.Controllers
{
	public class StatisticController : Controller
	{
		private Head.Services.StatisticService _service;
		public StatisticController(ILogger<StatisticController> logger, Head.Services.StatisticService service)
		{
			_service = service;
		}

		[HttpGet("statistics")]
		public IActionResult index()
		{
			Console.WriteLine("statistics");
			var tasks = Converter.ConvertTasksToUI(_service.GetStatistic());
			tasks.ForEach(task => task.Solution = "");
			string jsonString = JsonSerializer.Serialize(tasks, Options.JsonOptions());
			
			return new ContentResult
			{
				Content = jsonString,
				ContentType = "application/json",
				StatusCode = 200
			};
		}

	}
}
