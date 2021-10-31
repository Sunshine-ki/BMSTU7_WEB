using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ui.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

using bl;
using ui.dto;
using ui.Helpers;

namespace ui.Controllers
{
	public class AuthorizationController : Controller
	{	
		private ILogger<AuthorizationController> _logger;
		private Head.Services.UserService _userService;
		
		public AuthorizationController(ILogger<AuthorizationController> logger, Head.Services.UserService userService)
		{
			_logger = logger;
			_userService = userService;
		}

		[HttpPost]
		[Route("log_in")]
		public ActionResult<string> Authorization([FromBody] ui.Models.User user)
		{
			// TODO: Авторизация
			var result = _userService.LogIn(Converter.ConvertUserToBL(user));
			Console.WriteLine(result.Msg);

			if (result.returnValue != Head.Constants.OK)
			{
				// TODO: код возврата ?
				return JsonSerializer.Serialize(new ResultDTO() {Title = result.Msg}, Options.JsonOptions());
			}
			
			// TODO: код возврата ?
			return JsonSerializer.Serialize(new ResultDTO() {Title = "Успешная авторизация"}, Options.JsonOptions());
		}
	}
}
