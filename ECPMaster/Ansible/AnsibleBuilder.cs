using System.Collections.Generic;
using ECPMaster.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ECPMaster.Ansible
{
    public class AnsibleBuilder
    {
        private static AnsiblePlaybook _playbook;

        private AnsibleBuilder(AnsiblePlaybook playbook)
        {
            _playbook = playbook;
        }

        public static AnsibleBuilder BuildAnsiblePlaybook(
            string name, string hosts, bool become = false, string becomeUser = "ecp")
        {
            _playbook = new AnsiblePlaybook()
            {
                Content = new List<Dictionary<string, object>>() { }
            };
            _playbook.Content.Add(
                new Dictionary<string, object>
                {
                    { "name", name },
                    { "hosts", hosts }
                });
            if (!become) return new AnsibleBuilder(_playbook);

            _playbook.Content[0].Add("become", true);
            _playbook.Content[0].Add("become_user", becomeUser);

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddServiceModule(string taskName, string name, State state)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "service",
                        new ServiceModule(name, state)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddDockerContainerModule(string taskName, string name, string image, State state,
            List<string> ports = null, List<string> volumes = null, string restartPolicy = "no")
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "docker_container",
                        new DockerContainerModule(name, image, state, ports, volumes, restartPolicy)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }


        public AnsiblePlaybook Build()
        {
            return _playbook;
        }

        private static List<Dictionary<string, object>> GetTaskList()
        {
            var value = _playbook.Content[0]
                .GetValueOrDefault("tasks", null);

            if (value != null) return value;

            _playbook.Content[0].Add("tasks", new List<Dictionary<string, object>>());
            return _playbook.Content[0]["tasks"];
        }
    }
}