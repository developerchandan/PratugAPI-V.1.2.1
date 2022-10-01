using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class PortalLoginVm
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be between 6 and 50 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int isAdmin { get; set; } =0;
    }
}
