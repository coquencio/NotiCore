using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NotiCore.API.Infraestructure.Automapper;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Services.CoreServices.Implementation;
using NotiCore.API.Services.CoreServices;
using NotiCore.API.Services.ControllerServices.Implementation;
using NotiCore.API.Services.ControllerServices;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            
            // Core services
            services.AddHttpClient<IScraperService, ScraperService>();
            PythonService.SetupModules("newscatcher-0.2.0-py3-none-any.whl");
            services.AddSingleton<IPythonService, PythonService>();
            string encryptionKey = Configuration["EncryptionKey"];
            services.AddScoped<IEncryptionService>(s => new EncryptionService(encryptionKey));
            services.AddSingleton<IMLNewsWebsiteModel>(x => new MLNewsWebsiteModel(@"../NotiCoreML.Model/MLModel.zip"));

            // Controller Services
            services.AddScoped<ISourceService, SourceService>();
            services.AddScoped<IPredictNewsWebsiteService, PredictNewsWebsiteService>();
            services.AddScoped<ISubscriberService, SubscriberService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArticleService, ArticleService>();


            // Authentication and authorization
            var key = Encoding.UTF8.GetBytes(encryptionKey);
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Subscriber", policy => policy.RequireClaim("Subscriber"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
            });

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
