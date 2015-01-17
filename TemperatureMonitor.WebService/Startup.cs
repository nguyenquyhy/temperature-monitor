﻿using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace TemperatureMonitor.WebService
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
            app.Run(async req =>
            {
                await req.Response.WriteAsync("Hello world!");
            });
        }
    }
}
