using System.Collections.Generic;
using ECPMaster.Entities;

namespace ECPMaster.ViewModels
{
    public class ExecutionViewModel
    {
        public List<ECPLog> Logs { get; set; }
        public ECPDeployment Deployment { get; set; }
    }
}