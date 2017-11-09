using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebService;

namespace WebService
{
    public class WebService
    {
        private IWebHost _webHost;
        public WebService(string[] args)
        {
            this._webHost = BuildWebHost(args);
            this._webHost.Run();
        }

        //Build Server, See Startup.cs
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5001")
                .Build();
    }
}
