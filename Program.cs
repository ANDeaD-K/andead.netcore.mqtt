using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace andead.netcore.mqtt
{
    public class Program
    {
        private const string LISTEN_PORT_KEY_NAME = "listen-port";

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddCommandLine(args);
                })
                .ConfigureKestrel((hostingContext, options) =>
                {
                    int listenPort = hostingContext.Configuration.GetValue<int>(LISTEN_PORT_KEY_NAME, 5051);

                    options.Listen(IPAddress.Any, listenPort);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .UseStartup<Startup>();
    }
}
