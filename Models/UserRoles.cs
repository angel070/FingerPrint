using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class UserRoles
    {
        public int Id { get; set; }
        [Required(ErrorMessage="This field is required")]
        [Display(Name="Role")]
        public string Name { get; set; }
    }
}
