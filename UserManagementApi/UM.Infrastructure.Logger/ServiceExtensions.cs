using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace Hl.Infrastructure.Logger
{
    public static class ServiceExtensions
    {
        public static void AddLoggerLayer(this IHostBuilder host, IConfiguration configuration)
        {
            host.UseSerilog((context, config) => config
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Project", "HL")
                .WriteTo.Graylog(new GraylogSinkOptions
                {
                    HostnameOrAddress = configuration["Serilog:GrayLog"],
                    Port = Convert.ToInt32(configuration["Serilog:Port"]),
                    TransportType = TransportType.Udp
                }));
        }

    }
}