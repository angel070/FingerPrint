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
		public string Username { get; set; }
		public string Password { get; set; }
        public bool IsActive { get; set; }
		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set;}
		[Required(ErrorMessage = "Phone number is required")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
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
