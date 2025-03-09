using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECPMaster.Entities
{
    [Table("ECPLog")]
    public class ECPLog
    {
        public int Id { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        [Required]
        public string Line { get; set; }
        [Required]
        public int DeploymentId { get; set; }

        public ECPDeployment Deployment { get; set; }
    }
}