using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class FingerContext : DbContext
    {
		public FingerContext() : base("name=FingerContext"){ }
		public DbSet<Country> Countries { get; set;}
        public DbSet<Event> Events { get; set;}
        public DbSet<Branch> Branches { get; set;}
        public DbSet<Department> Departments { get; set;}
        public DbSet<Staff> Staffs { get; set;}
        public DbSet<UserRoles> Roles{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Holiday> Holdays { get; set; }
        public DbSet<TimeInAndOut> TimeInAndOuts { get; set; }
        public DbSet<WorkingHours> WorkingHourse { get; set; }
        public DbSet<StaffCheckInAndOutReport> StaffCheckInAndOutReports { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Leave> Leaves { get; set; }

	}
}
