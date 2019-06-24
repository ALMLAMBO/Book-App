using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookApp {
	public class Startup {
		public static IConfiguration Configuration { get; set; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();

			string connectionString = 
				Configuration["connectionStrings:bookDbConnectionString"];

			services.AddDbContext<BookDbContext>(c => {
				c.UseSqlServer(connectionString);
			});

            services.AddScoped<ReflectiveRepository<Author>>();
            services.AddScoped<ReflectiveRepository<Book>>();
            services.AddScoped<ReflectiveRepository<Category>>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, BookDbContext bookContext) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			//app.Run(async (context) => {
			//	await context.Response.WriteAsync("Hello World!");
			//});

			//bookContext.SeedDataContext();

			app.Use(async (context, next) => {
				//await next();
				if(context.Response.StatusCode == 404 && 
					!Path.HasExtension(context.Request.Path.Value)) {
			
					context.Request.Path = "/index.html";
					await next();
				}
			});

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseMvc();
		}
	}
}
