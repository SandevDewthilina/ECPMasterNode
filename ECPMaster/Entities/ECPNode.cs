using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECPMaster.Entities
{
    [Table("ECPNode")]
    public class ECPNode
    {
        public int Id { get; set; }
        [Required]
        public string NodeIdentifier { get; set; }
        [Required]
        public AgentState NodeState { get; set; }
        [Required]
        public string OperatingSystem { get; set; }
        [Required]
        public string IPv4 { get; set; }

        public string Domain { get; set; }
    }
    
    public enum AgentState
    {
        Available,
        Down,
        Stopped
    }
}