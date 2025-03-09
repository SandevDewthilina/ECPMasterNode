using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECPMaster.Entities
{
    [Table("ECPDbConfig")]
    public class ECPDbConfig
    {
        public int Id { get; set; }
        [Required]
        public int Port { get; set; }
        public string Password { get; set; }
        public string User { get; set; } = "root";
        [Required]
        public string DbBackupFileName { get; set; }
    }
}