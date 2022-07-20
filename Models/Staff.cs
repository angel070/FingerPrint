using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FingerPrint.Models
{
    public class Staff
    {
        public int Id { get; set; }
		[Remote("CheckStaffIdExists", "Staff", AdditionalFields = "StaffId_clone", ErrorMessage = "Id already exists!", HttpMethod = "POST")]
		[Required(ErrorMessage = "This field is required")]
		public string Staff_id { get; set; }
		[NotMapped]
		public string StaffId_clone { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public string Phone_no { get; set; }
        //Foreign key
        [Display(Name ="Country")]
		[Required(ErrorMessage = "This field is required")]
		public int CountryId { get; set; }
        public Country Country { get; set; }
        [Display(Name = "Branch")]
		[Required(ErrorMessage = "This field is required")]
		public int BranchId { get; set; }
        public Branch Branch { get; set; }
        [Display(Name = "Department")]
		[Required(ErrorMessage = "This field is required")]
		public int DepartmentId {get; set;}
        public Department Department { get; set; }

		[Remote("CheckEmailExists", "Staff", AdditionalFields = "Email_clone", ErrorMessage = "Email already exists!", HttpMethod = "POST")]
		[Required(ErrorMessage = "This field is required")]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
		[NotMapped]
		public string Email_clone { get; set; }
        public string Fingerprint { get; set; }
    }
}
