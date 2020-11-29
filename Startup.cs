using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NETCore_Demo.Models;

namespace NETCore_Demo
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
			services.AddControllersWithViews();
			services.AddDbContext<EmployeeContext>(options =>
			{
				//设置打印出控制台输出sql语句并显示参数化查询的参数
				options.UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging();
				options.UseSqlServer(Configuration.GetConnectionString("DevConnection"));
			});
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
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Employees}/{action=Index}/{id?}");
			});
		}

		//将SQL执行语句输出到控制台
		public static readonly ILoggerFactory ConsoleLoggerFactory =
			LoggerFactory.Create(builder =>
			{
				builder.AddFilter((category, level) =>
						category == DbLoggerCategory.Database.Command.Name
						&& level == LogLevel.Information)
					.AddConsole();
			});
	}
}
