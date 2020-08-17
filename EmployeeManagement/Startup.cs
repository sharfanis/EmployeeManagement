using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDbConnection")));


            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>;

            services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddMvcCore();

            /// basically saying that if home controller where the Employee Respository is sitting ,
            /// if you want to access IEmployee Respository  , then inject MockEmployeeRepository.

            //With this one line of code we can tell .NET core that if we  are asking from
            // IEmployeeREPO give them implementation of MockEmployeeRepo but we don't want inhouse storage
            //this time we want SQLEmployee Repo so change this line.
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            //This is power of Repo pattern with Dependency Injection.

            //Add Scoped so that the instance of SQLEMPLOYEEREPO to be alive during one 
            //HTTP Request and a new instance for another HTTP Request.

            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //If we want to set up a custom UAT environment then , 
            
            //if(env.IsEnvironment("UAT")) and then check in LaunchSetting.JSON if they have a UAT.


            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();

                //    developerExceptionPageOptions.SourceCodeLineCount = 10;

                //app.UseDeveloperExceptionPage(developerExceptionPageOptions);

                app.UseDeveloperExceptionPage();

            } else if ( env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            {
                app.UseExceptionHandler("/Error");
                // app.UseStatusCodePagesWithRedirects("/Error/{0}");    Avoid this because it is deceving in network tab.

                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }


            // This is for custom passing of HTML file.
            //DefaultFilesOptions df = new DefaultFilesOptions();
            //df.DefaultFileNames.Clear();
            //df.DefaultFileNames.Add("foo.html");


            // Pass the df val for hosting the custom HTML file.
            // We can use useFileServer middleware instead of below two .

            //FileServerOptions fs = new FileServerOptions();
            //fs.DefaultFilesOptions.DefaultFileNames.Clear();
            //fs.DefaultFilesOptions.DefaultFileNames.Add("foo.html");

           // app.UseFileServer();
            
            //app.UseDefaultFiles(df);

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

         

           app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    //await context.Response.
                    //WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);

                    //throw new Exception("Somne error can't work ");

                    await context.Response.
                    WriteAsync(_config["MyKey"]);

                    await context.Response.
                WriteAsync(" Hosting environment is :" + env.EnvironmentName );



                });
            });
        }
    }
}
