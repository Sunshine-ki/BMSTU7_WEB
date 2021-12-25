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
using ui.dto;

namespace ui.Controllers
{
	public class RegistrationController : Controller
	{
		private Head.Services.UserService _userService;
		ILogger<RegistrationController> _logger;
		public RegistrationController(ILogger<RegistrationController> logger, Head.Services.UserService userService)
		{
			_userService = userService;
			_logger = logger;
		}

		[HttpPost("registration")]
		public IActionResult Registration([FromBody] ui.Models.User newUser)
		{
			Console.WriteLine(newUser);
			var user = Converter.ConvertUserToBL(newUser);
			var result = _userService.AddUser(user);

			if (result.returnValue != Head.Constants.OK)
			{
				return new ContentResult
				{
					Content = JsonSerializer.Serialize(new ResultDTO() {Title = result.Msg}, Options.JsonOptions()),
					ContentType = "application/json",
					StatusCode = 418
				};
			}
			
			Console.WriteLine("Ok");

			var userId = _userService.GetIdByLogin(user);
			Console.WriteLine($"{userId}");
			HttpContext.Session.SetString("id", Convert.ToString(userId));

			return new ContentResult
			{
				Content = JsonSerializer.Serialize(new ResultDTO() {Title = "You have successfully registered"}, Options.JsonOptions()),
				ContentType = "application/json",
				StatusCode = 200
			};
			
		}

		[HttpPatch("update_user/{login}")]
		public string UpdateUser(string login, [FromBody] ui.Models.User newUser)
		{
			Console.WriteLine(newUser);

			var user = Converter.ConvertUserToBL(newUser);
			var result =_userService.UpdateUser(login, user);

			if (result.returnValue != Head.Constants.OK)
			{
				return JsonSerializer.Serialize(new ResultDTO() {Title = result.Msg}, Options.JsonOptions());
			}

			return JsonSerializer.Serialize(new ResultDTO() {Title = "Successful update"}, Options.JsonOptions());
		}

	}
}
