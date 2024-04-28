using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ECPMaster.Models
{
    public class AnsiblePlaybook
    {
        public List<Dictionary<string, dynamic>> Content { get; set; }
        
        public string ToYaml()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            return serializer.Serialize(Content);
        }

        public async Task SaveFile()
        {
            var yaml = ToYaml();
            
            await File.WriteAllTextAsync("/temp.yml", yaml);
        }

        public async Task Execute()
        {
            
        }
    }

    public class ServiceModule
    {
        public ServiceModule(string name, State state)
        {
            Name = name;
            State = state;
        }

        public string Name { get; set; }
        public State State { get; set; }
    }

    public class DockerContainerModule
    {
        public DockerContainerModule(string name, string image, State state, List<string> ports, List<string> volumes,
            string restartPolicy)
        {
            Name = name;
            Image = image;
            State = state;
            RestartPolicy = restartPolicy;
            Ports = ports;
            Volumes = volumes;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public State State { get; set; }
        public string RestartPolicy { get; set; }
        public List<string> Ports { get; set; }
        public List<string> Volumes { get; set; }
    }

    public enum State
    {
        absent,
        present,
        stopped,
        started
    }
}