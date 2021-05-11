using System;
using Cleverbit.CodingTask.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cleverbit.CodingTask.Tests.Shared {

    public abstract class AppFactory : WebApplicationFactory<Startup> {

        protected override IHostBuilder CreateHostBuilder() {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

            return base.CreateHostBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureTestServices(MockServices);
        }

        protected virtual void MockServices(IServiceCollection services) {
        }
    }

}