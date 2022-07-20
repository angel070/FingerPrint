namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingtotalhours : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "TotalStaffHours", c => c.Time(precision: 7));
            DropColumn("dbo.StaffCheckInAndOutReports", "life");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "life", c => c.Int());
            DropColumn("dbo.StaffCheckInAndOutReports", "TotalStaffHours");
        }
    }
}
