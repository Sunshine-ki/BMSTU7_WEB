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
		[Route("login")]
		public ActionResult<string> Authorization([FromBody] ui.Models.User user)
		{
			// TODO: Авторизация
			var result = _userService.LogIn(Converter.ConvertUserToBL(user));
			Console.WriteLine(result.Msg);

			if (result.returnValue != Head.Constants.OK)
			{
				// TODO: код возврата ?
				// https://httpstatuses.com/
		        this.HttpContext.Response.StatusCode = 418;
				return JsonSerializer.Serialize(new ResultDTO() {Title = result.Msg}, Options.JsonOptions());
			}
			
			return JsonSerializer.Serialize(new ResultDTO() {Title = "Успешная авторизация"}, Options.JsonOptions());
		}


		[HttpGet]
		[Route("login")]
		public ActionResult<string> Authorization()
		{
			return "Please, login";
		}


	}
}
