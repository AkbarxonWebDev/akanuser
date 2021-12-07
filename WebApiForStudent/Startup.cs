using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.App_Data;
using WebApiForStudent.App_Data.Address;
using WebApiForStudent.App_Data.Address.Concrete;
using WebApiForStudent.App_Data.Course_App;
using WebApiForStudent.App_Data.Course_App.Concrete;
using WebApiForStudent.App_Data.Student;
using WebApiForStudent.App_Data.Student.Concrete;
using WebApiForStudent.App_Data.Teacher_App;
using WebApiForStudent.App_Data.Teacher_App.Concrete;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Utilities;
using Microsoft.AspNetCore.Mvc.Authorization;
using WebApiForStudent.App_Data.Account;

namespace WebApiForStudent
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
            services.AddControllers(
                //config => config.Filters.Add(typeof(MyExceptionFilter))
                config=>config.Filters.Add(new AuthorizeFilter())
                )
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();

            //
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAddress, AddressService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //database
            services.AddDbContextPool<AppDbContext>(options => 
            options.UseSqlServer(
                Configuration.
                GetConnectionString("data")));

            //step - 1 Install Package Microsoft.AspNetCore.Identity.EntityFrameworkCore

            //step - 2
            //Identity Add User
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //step - 3
            services.ConfigureApplicationCookie(opttions =>
                      opttions.Events = new CookieAuthenticationEvents()
                      {
                          //Authentication
                          OnRedirectToLogout = login =>
                                               {
                                                   login.HttpContext.Response.StatusCode = 403;
                                                   return Task.CompletedTask;
                                               },
                          //Authorization
                          OnRedirectToAccessDenied = access =>
                                               {
                                                   access.HttpContext.Response.StatusCode = 401;
                                                   return Task.CompletedTask;
                                               }

                      });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo { 
                        Title = "Web Api", 
                        Version = "v1" 
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiForStudent v1"));
            }

            app.UseExceptionHandler(options =>
                                      {
                                          options.Run(async context =>
                                                          {
                                                              context.Response.StatusCode = 500;
                                                              context.Response.ContentType = "application/json";
                                                              var ex = context.Features.Get<IExceptionHandlerFeature>();
                                                              if (ex!=null)
                                                              {
                                                                  await context.Response.WriteAsync("I am coming from Start up.....!"+ex.Error.Message.ToString());
                                                              }
                                                          });
                                      });

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            //step - 4
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
