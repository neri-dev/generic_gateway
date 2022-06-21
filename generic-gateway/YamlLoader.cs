using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace generic_gateway
{
    internal class YamlLoader
    {
        public YamlLoader()
        {
        }

        internal GatewaySettings Load(string yamlFile)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance).Build();

            //yml contains a string containing your YAML
            return deserializer.Deserialize<GatewaySettings>(File.ReadAllText(yamlFile));
        }
    }
}