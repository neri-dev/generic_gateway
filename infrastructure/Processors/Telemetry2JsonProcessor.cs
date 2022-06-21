using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infrastructure.Processors
{
	public class Telemetry2JsonProcessor : IProcessor
	{
		public Telemetry2JsonProcessor(Dictionary<string, object> settings)
		{
		}

        public Telemetry2JsonProcessor()
        {

        }

        public object Process(object obj)
        {
            // this code simulates the conversion between AeroICD (telemetry) into a generic Message object
            if(obj is GcsLightsRep gcsLightsRep)
            {
                var payload = new
                {
                    gcsLightsRep.IsNavLightsOn,
                    gcsLightsRep.IStrobLightsOn
                };

                var message = new Message("GcsLightsRep", JsonConvert.SerializeObject(payload));
                return message;
            };

            return "";
        }
    }
}

