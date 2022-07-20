using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class Branch
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Branch Name is required")]
		public string Name { get; set; }

        //Foreign Key
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        //Load Brach with their countries
        public Country Country { get; set; }

    }
}
