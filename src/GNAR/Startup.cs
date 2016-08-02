using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GNAR.Services;
using GNAR.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using GNAR.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GNAR
{
	public class Startup
	{
		private IHostingEnvironment _env;

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
			_env = env;
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton(Configuration);

			if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
			{
				services.AddScoped<IMailService, DebugMailService>();
			}

			services.AddIdentity<PetUser, IdentityRole>(config =>
			{
				config.User.RequireUniqueEmail = true;
				config.Password.RequiredLength = 8;
				config.Cookies.ApplicationCookie.LoginPath = "/auth/login";
				config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
				{
					OnRedirectToLogin = async ctx =>
					{
						if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
						{
							ctx.Response.StatusCode = 401;
						}
						else
						{
							ctx.Response.Redirect(ctx.RedirectUri);
						}
						await Task.Yield();
					}
				};
			}).AddEntityFrameworkStores<PetContext>();

			// Add framework services.
			services.AddDbContext<PetContext>();
			services.AddScoped<IGnarRepository, GnarRepository>();
			services.AddTransient<GeoCoordsService>();
			services.AddTransient<PetContextSeedData>();

			services.AddMvc(config =>
			{
				if (_env.IsProduction())
				{
					config.Filters.Add(new RequireHttpsAttribute());
				}
			})
			.AddJsonOptions(config =>
			{
				config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app,
			IHostingEnvironment env,
			PetContextSeedData seeder,
			ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();

				loggerFactory.AddDebug(LogLevel.Information);
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");

				loggerFactory.AddDebug(LogLevel.Error);
			}

			app.UseStaticFiles();

			app.UseIdentity();

			Mapper.Initialize(config =>
			{
				config.CreateMap<PetViewModel, Pet>().ReverseMap();
				config.CreateMap<FosterViewModel, Pet>().ReverseMap();
			});

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=GNAR}/{action=Index}/{id?}");
			});

			seeder.EnsureSeedData().Wait();
		}
	}
}
