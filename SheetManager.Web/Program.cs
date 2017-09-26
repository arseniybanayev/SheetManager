using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SheetManager.Web.Data;
using System;

namespace SheetManager.Web
{
	public class Program
	{
		public static void Main(string[] args) {
			var host = BuildWebHost(args);
			using (var scope = host.Services.CreateScope()) {
				var services = scope.ServiceProvider;
				try {
					var context = services.GetRequiredService<SheetsContext>();
					DbInitializer.Initialize(context);
				} catch (Exception e) {
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(e, "An error occurred while seeding the database.");
				}
			}
			
			host.Run();
		}

		public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.Build();
	}
}