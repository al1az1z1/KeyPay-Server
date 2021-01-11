using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KeyPay.Common.Helpers;
using KeyPay.Data.DatabaseContext;
using KeyPay.Repo.Infrastructure;
using KeyPay.Services.Seed.Interface;
using KeyPay.Services.Seed.Services;
using KeyPay.Services.Site.Admin.Auth.Interface;
using KeyPay.Services.Site.Admin.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KeyPay.Presentation
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

            //automapper
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<ISeedService, SeedService>();



            services.AddControllers();
            services.AddCors();

            services.AddScoped<IUnitOfWork<KeyPayDbContext>, UnitOfWork<KeyPayDbContext>>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();




            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
                    (Encoding.ASCII.GetBytes(Configuration.GetSection("AppSetting:Token").Value)),
                    // two bottom properties becuase of local Host are false maybe change in real Host or publish
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            //nswag
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "Site";
                document.ApiGroupNames = new[] { "Site" };

                document.PostProcess = d =>
                {
                    d.Info.Title = "Hello Ali";
                    d.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "OpenApiContact",
                        Email = string.Empty,
                        Url = "Https://OpenApiContact.com/",
                    };
                    d.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "openapilicenese",
                        Url = "Https://openapilicenese.com/",

                    };

                   
                };

                document.AddSecurity(name: "JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}.",

                });

                document.OperationProcessors.Add(
                    new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor(name: "JWT"));
                //      new OperationSecurityScopeProcessor("bearer"));
            });
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "Api";
                document.ApiGroupNames = new[] { "Api" };
            });





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedService seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddAppError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });

                });
            }

            app.UseHttpsRedirection();

            //seeder.SeedUsers();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseOpenApi();

            app.UseSwaggerUi3();

            #region swagger with configue
            // serve documents (same as app.UseSwagger())
            //app.UseOpenApi(
            ////    option => 
            ////{ option.DocumentName = "swagger";
            ////    option.Path = "/swagger/v1/swagger.json";
            ////}
            //)
            //;

            //app.UseSwaggerUi3(
            ////    option =>
            ////{
            ////    option.Path = "/Helper";
            ////    option.DocumentPath = "/swagger/v1/swagger.json";
            ////}
            //);
            // serve Swagger UI

            //app.UseReDoc(option =>
            //{
            //    option.Path = "/redoc";
            //    option.DocumentPath = "/swagger/v1/swagger.json";
            //});
            #endregion /swagger with configue

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
