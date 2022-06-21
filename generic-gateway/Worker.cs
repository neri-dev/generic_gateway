using infrastructure;
using infrastructure.Exporters;
using infrastructure.Importers;
using infrastructure.Processors;

namespace generic_gateway;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        GatewayProcess gateway = new();

        gateway.Register(new TelemetryImporter())
               .Register(new Telemetry2JsonProcessor())
               .Register(new KafkaExporter());



        gateway.Start();
    }
}

