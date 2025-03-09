using System.Collections.Generic;
using ECPMaster.Entities;

namespace ECPMaster.ViewModels
{
    public class DeploymentViewModel
    {
        // create form
        public int NodeId { get; set; }
        public string ArtifactName { get; set; }
        public string SystemName { get; set; }
        public int SystemPort { get; set; }
        public string SubDomain { get; set; }
        public string DbBackupFileName { get; set; }
        public string DbName { get; set; }
        public int MySqlPort { get; set; }
        public string MySqlUser { get; set; }
        public string MySqlPassword { get; set; }
        
        // view data
        public List<ECPArtifact> Artifacts { get; set; }
        public List<ECPNode> Nodes { get; set; }
    }
}