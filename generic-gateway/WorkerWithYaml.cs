using System.Reflection;
using infrastructure;
using infrastructure.Exporters;
using infrastructure.Importers;
using infrastructure.Processors;

namespace generic_gateway;

public class WorkerWithYaml : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public WorkerWithYaml(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Load yaml settings file
        var settings = new YamlLoader().Load("sender_gw.yaml");

        // Dynamically load Importer, Processors and Exporter from their assembly
        var assembly = GetAssemblyByName("infrastructure");

        var importer = CreateInstanceOf<IImporter>(settings.Importer, assembly);
        var exporter = CreateInstanceOf<IExporter>(settings.Exporter, assembly);
        var processors = settings.Processors
            .Select(processorClass => CreateProcessor(processorClass, assembly));

        // Create gateway service
        GatewayProcess gateway = new();

        // Assign Importer, Processors and Exporter
        gateway.Register(importer)
               .Register(processors)
               .Register(exporter);

        // Start the service
        gateway.Start();
    }

    private IProcessor CreateProcessor(object name, Assembly assembly)
    {
        var processor = GetType<IProcessor>(assembly, (string)name);
        return processor;
    }

    private T CreateInstanceOf<T>(Dictionary<string, object> settings, Assembly assembly) where T : class
    {
        var name = settings["class"];
        var instance = GetType<T>(assembly, (string)name, settings);
        return instance;
    }

    private T GetType<T>(Assembly assembly, string name, Dictionary<string, object> settings = null) where T : class
    {
        var type = assembly.GetExportedTypes().Where(type => type.Name == name).FirstOrDefault();
        return type == null ? null : (T)Activator.CreateInstance(type, settings);
    }
  
    Assembly GetAssemblyByName(string name)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SingleOrDefault(assembly => assembly.GetName().Name == name);
    }
}

