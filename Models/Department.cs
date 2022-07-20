using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class Department
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Department Name is required")]
		public string Name { get; set; }
        //Foreign Key
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        //Get Departments with their brances
        public Branch Branch { get; set; }

    }
}
