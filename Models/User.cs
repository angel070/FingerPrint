using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class User
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
        [Display(Name ="User Name")]
		[Required(ErrorMessage = "UserName is required")]
		public string Username { get; set; }
        [DataType(DataType.Password)]
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }
        public bool IsActive { get; set; }
        //Foreign Key
        [Display(Name ="Roles")]
		[Required(ErrorMessage = "User Role is required")]
		public int UserRolesId { get; set; }
        public UserRoles UserRoles { get; set; }
		[Display(Name ="Branch")]
		[Required(ErrorMessage = "User Branch is required")]
		public int BranchId { get; set; }
		public Branch Branch { get; set; }
    }
}
