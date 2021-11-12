using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

using Head;
using Microsoft.OpenApi.Models;

namespace ui
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});


            // // установка конфигурации подключения
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options => //CookieAuthenticationOptions
            //     {
            //         options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login");
            //     });
				
			services.AddControllersWithViews();

			// https://localhost:5001/swagger/index.html
		    services.AddSwaggerGen();

			AddTransients(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();    // аутентификация
			app.UseAuthorization();     // авторизация

			app.UseSession();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			// app.UseSwagger();

			// app.UseSwagger(c => c.RouteTemplate = "api/v1");

			// // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
			// app.UseSwaggerUI(c =>
			// {
			// 	c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			// 	c.RoutePrefix = "docs";
			// });

			app.UseSwagger(c =>
				{
					c.RouteTemplate = "swagger/{documentName}/swagger.json";
					c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
					{
						swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://localhost/api/v1" } };
					});
				});

			app.UseSwaggerUI();

			// app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				// If you want check old route
				// endpoints.MapControllerRoute(
				// 	name: "default",
				// 	pattern: "{controller=Home}/{action=Index}/{id?}");
			});
			

			// app.UseEndpoints(endpoints =>
			// {
			// 	endpoints.MapControllerRoute(
			// 		name: "default",
			// 		pattern: "{controller=Home}/{action=Index}/{id?}");
			// });
		}

		private void AddTransients(IServiceCollection services)
		{
			services.AddTransient<db.IRepositoryUser, db.PostgreSQLRepositoryUser>();
			services.AddTransient<db.IRepositoryTask, db.PostgreSQLRepositoryTask>();
			services.AddTransient<db.IRepositoryCompletedTask, db.PostgreSQLRepositoryCompletedTask>();
			services.AddTransient<bl.IFacade, db.ConFacade>();
			services.AddTransient<Head.Facade>();
			services.AddTransient<Head.Services.StatisticService>();
			services.AddTransient<Head.Services.TaskService>();
			services.AddTransient<Head.Services.UserService>();
		}
	}
}
