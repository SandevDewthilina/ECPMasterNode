using System.Collections.Generic;
using ECPMaster.Ansible;
using ECPMaster.DbContext;
using ECPMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECPMaster.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ECPDbContext _db;

        public HomeController(ILogger<HomeController> logger, ECPDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }

        public object Yaml()
        {
            return AnsibleBuilder.BuildAnsiblePlaybook("Deploy docker container", "rl",true)
                .AddServiceModule("Ensure docker is running", "docker", State.started)
                .AddDockerContainerModule("Deploy Docker container", "nginx-test", "nginx:latest",State.started, ports: new List<string>() {"80:80"}).Build();
        }
    }
}
