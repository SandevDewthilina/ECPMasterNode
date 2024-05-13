using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECPMaster.Entities
{
    [Table("ECPArtifact")]
    public class ECPArtifact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Tag { get; set; }
        [Required]
        public string Url { get; set; }
    }
}