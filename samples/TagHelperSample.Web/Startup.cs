// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TagHelperSample.Web.Services;

namespace TagHelperSample.Web
{
    public class Startup
    {
        // Set up application services
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<MoviesService>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole((name, logLevel) =>
                name.StartsWith("Microsoft.AspNetCore.Mvc.TagHelpers", StringComparison.OrdinalIgnoreCase)
                || (name.StartsWith("Microsoft.Net.Http.Server.WebListener", StringComparison.OrdinalIgnoreCase)
                    && logLevel >= LogLevel.Information));

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseDefaultHostingConfiguration(args)
                .UseIISPlatformHandlerUrl()
                .UseServer("Microsoft.AspNetCore.Server.Kestrel")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
