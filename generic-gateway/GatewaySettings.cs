namespace generic_gateway
{
    public class GatewaySettings
    {
        public Dictionary<string, object> Importer { get; set; }

        public List<string> Processors { get; set; }

        public Dictionary<string, object> Exporter { get; set; }

    }
}

/*
importer:
    name: telemetry
    ip: 224.13.14.16
    port: 50220
    nic: localhost

processors:
    - name: telemetry2json

exporter:
    name: kafka
    ip: localhost
    nic: localhost
    port: 9092
    clientId: demo
    type_topic_map:
        GcsLightsRep : telemetry_lights
        GcsFastInsRpt : telemetry_fast_ins
 
 */