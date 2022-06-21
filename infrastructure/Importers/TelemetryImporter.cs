using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace infrastructure.Importers
{
    public class TelemetryImporter : IImporter
    {
        private bool _canceling;

        public event EventHandler<object> DataReady = delegate { };

        public TelemetryImporter(Dictionary<string, object> settings)
        {
        }

        public TelemetryImporter()
        {
        }

        public void Start()
        {
            // Simulates UAV telemetry report
            Task.Factory.StartNew(new Action(() =>
            {
                Random random = new Random();

                while (!_canceling)
                {
                    var value = random.Next(0, 10000) % 2 == 0;
                    var gcsLightsRep = new GcsLightsRep(value, value);

                    DataReady(this, gcsLightsRep);

                    Task.Delay(1000);
                }
            }));
        }

        public void Stop()
        {
            _canceling = true;
        }
    }
}

