using System;
using System.Collections.Generic;
using infrastructure.Exporters;

namespace infrastructure
{
	public class GatewayProcess 
	{
        private  IImporter _importer;
        private  IExporter _exporter;
        private readonly List<IProcessor> _processors = new List<IProcessor>();

		public GatewayProcess()
		{
		}


        public GatewayProcess Register(IImporter importer)
        {
            _importer = importer;
            importer.DataReady += OnImporterDataReady;

            return this;
        }

        public GatewayProcess Register(IProcessor processor)
        {
            _processors.Add(processor);

            return this;
        }

        public GatewayProcess Register(IEnumerable<IProcessor> processors)
        {
            _processors.AddRange(processors);

            return this;
        }

        public void Register(IExporter exporter)
        {
            _exporter = exporter;
        }

        private void OnImporterDataReady(object sender, object data)
        {
            foreach (var processor in _processors)
            {
                data = processor.Process(data);
            }

            _exporter.Export(data);
        }

        public void Start()
        {
            _importer.Start();
        }

        public void Stop()
        {
            _importer.Stop();
        }
    }
}

