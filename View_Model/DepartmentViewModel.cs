using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerPrint.View_Model
{
    public class DepartmentViewModel
    {
        public Department _department { get; set; }
        public IEnumerable<Department> department { get; set; }
        public IEnumerable<Branch> branch { get; set; }
        public IEnumerable<StaffCheckInAndOutReport> Reports { get; set; }
    }
}