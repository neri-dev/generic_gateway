using generic_gateway;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<WorkerWithYaml>();
    })
    .Build();

await host.RunAsync();