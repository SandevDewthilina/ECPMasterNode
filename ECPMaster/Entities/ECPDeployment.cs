using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECPMaster.Entities
{
    [Table("ECPDeployment")]
    public class ECPDeployment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Port { get; set; }
        public string SubDomain { get; set; }
        [Required]
        public DeploymentType Type { get; set; }
        [Required]
        public int ArtifactId { get; set; }
        [Required]
        public int NodeId { get; set; }
        [Required]
        public int DbConfigId { get; set; }

        public ECPArtifact Artifact { get; set; }
        public ECPNode Node { get; set; }
        public ECPDbConfig DbConfig { get; set; }
        public DeploymentStatus Status { get; set; }
    }

    public enum DeploymentType
    {
        Contained,
        External
    }

    public enum DeploymentStatus
    {
        Complete,
        Failed,
        New,
        Pending
    }
}