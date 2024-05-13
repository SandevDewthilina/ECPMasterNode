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

        public async Task SaveFile(string path)
        {
            var yaml = ToYaml();
            Directory.CreateDirectory(path);
            await File.WriteAllTextAsync(Path.Combine(path, "playbook.yaml"), yaml);
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

    public class BuiltInFileModule
    {
        public BuiltInFileModule(string path, State state, string mode)
        {
            Path = path;
            State = state;
            Mode = mode;
        }

        public string Path { get; set; }
        public State State { get; set; }
        public string Mode { get; set; }
    }

    public class DockerContainerModule
    {
        public DockerContainerModule(string name, string image, State state, List<string> ports, List<string> volumes,
            string restartPolicy, Dictionary<string, string> etcHosts)
        {
            Name = name;
            Image = image;
            State = state;
            RestartPolicy = restartPolicy;
            Ports = ports;
            Volumes = volumes;
            Etc_hosts = etcHosts;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public State State { get; set; }
        public string RestartPolicy { get; set; }
        public List<string> Ports { get; set; }
        public List<string> Volumes { get; set; }
        public Dictionary<string, string> Etc_hosts { get; set; }
    }

    public class TemplateModule
    {
        public TemplateModule(string src, string dest)
        {
            Src = src;
            Dest = dest;
        }

        public string Src { get; set; }
        public string Dest { get; set; }
    }

    public class DockerImageModule
    {
        public DockerImageModule(string buildPath, string name, string tag, bool push)
        {
            Build = new Dictionary<string, string>() { { "path", buildPath } };
            Name = name;
            Tag = tag;
            Push = push;
            Source = "build";
        }

        public Dictionary<string, string> Build { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public bool Push { get; set; }
        public string Source { get; set; }
    }

    public class DockerLoginModule
    {
        public DockerLoginModule(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class GetUrlModule
    {
        public GetUrlModule(string url, string dest)
        {
            Url = url;
            Dest = dest;
        }

        public string Url { get; set; }
        public string Dest { get; set; }
    }

    public enum State
    {
        absent,
        present,
        stopped,
        started,
        directory
    }
}