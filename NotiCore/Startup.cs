using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NotiCore.API.Infraestructure.Automapper;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Services;
using NotiCore.API.Services.Implementation;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
             .WriteTo
             .MSSqlServer(
                 connectionString: configuration.GetConnectionString("DBContection"),
                 sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents" , AutoCreateSqlTable = true},
                 restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
             .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RequestsMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotiCore", Version = "v1" });
            });
            services.AddDbContext<DataContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DBContection")));

            services.AddHttpClient<IScraperService, ScraperService>();

            services.AddScoped<IPredictNewsWebsiteService, PredictNewsWebsiteService>();

            services.AddScoped<ISourceService, SourceService>();

            services.AddSingleton<IMLNewsWebsiteModel>(x => new MLNewsWebsiteModel(@"../NotiCoreML.Model/MLModel.zip"));
            
            // Python Setup
            PythonService.SetupModules("newscatcher-0.2.0-py3-none-any.whl");

            services.AddSingleton<IPythonService, PythonService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotiCore v1"));
            }
            loggerFactory.AddSerilog();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
