using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Users.API.DataServices;
using Users.API.Helpers;

namespace Users.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Use SQL Server
            var connectionString = Configuration["ConnectionStrings:UsersDBConnectionString"];
            services.AddDbContext<UsersContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IUsersRepository, UsersRepository>();

            //Automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #region SWAGGER

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                        $"UsersOpenAPISpecification",
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "Users API",
                            Version = "v1",
                            Description = "Through this API users logic is exposed.",
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                            {
                                Email = "lcc.omar.aceves@gmail.com",
                                Name = "Omar Aceves",
                                Url = new Uri("https://github.com/omaraceves")
                            },
                            License = new Microsoft.OpenApi.Models.OpenApiLicense()
                            {
                                Name = "MIT License",
                                Url = new Uri("https://opensource.org/licenses/MIT")
                            }
                        });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            
            app.UseMvc();

            app.UseSwagger();


            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/UsersOpenAPISpecification/swagger.json",
                    "Users API");

                setupAction.RoutePrefix = "";

                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
            });

        }
    }
}
