using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.View_Model
{
    public class CheckIdViewModel
    {
        [Display(Name = "Staff_Id")]

        public string Userid { get; set; }
    }
}
