using System;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Host.Auth;
using Cleverbit.CodingTask.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;
using Cleverbit.CodingTask.Application.DTO.Authentication;
using Cleverbit.CodingTask.Application.ServiceImplementations;
using Cleverbit.CodingTask.Application.Services;
using Cleverbit.CodingTask.Data.DateAndTime;
using Cleverbit.CodingTask.Host.Filters;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using AuthenticationService = Cleverbit.CodingTask.Application.ServiceImplementations.AuthenticationService;
using IAuthenticationService = Cleverbit.CodingTask.Application.Services.IAuthenticationService;

namespace Cleverbit.CodingTask.Host {

    public class Startup {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration) {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services
                .AddDbContext<CodingTaskContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // utility services
            services.AddSingleton<IHashService>(new HashService(configuration.GetSection("HashSalt").Get<string>()));
            services.AddSingleton<IClock, DefaultClock>();

            // application services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IScoreGenerator, RandomScoreGenerator>();

            services.AddControllers(options =>
                    options.Filters.Add(new ApiErrorFilter()))
                .AddFluentValidation(f => {
                    f.RegisterValidatorsFromAssemblyContaining<SignInValidator>();
                    f.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    f.ImplicitlyValidateChildProperties = true;
                    f.ImplicitlyValidateRootCollectionElements = true;
                });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddSwaggerGen();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHashService hashService, CodingTaskContext context, 
            ILoggerFactory loggerFactory, IClock clock) {

            // initialize database
            try {
                context.Initialize(hashService, clock).Wait();
            } catch (Exception ex) {
                var logger = loggerFactory.CreateLogger("startup");
                logger.LogError(ex, "An error occurred creating the DB.");
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Views")),
                RequestPath = "/Views"
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cleverbit V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }

}