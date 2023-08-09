using System;
using System.ComponentModel.DataAnnotations;

namespace ECPMaster.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public String RoleName { get; set; }
    }
}
