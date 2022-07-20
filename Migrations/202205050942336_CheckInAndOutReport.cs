namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckInAndOutReport : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StaffCheckInAndOutReports", "Department");
            DropColumn("dbo.StaffCheckInAndOutReports", "Branch");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "Branch", c => c.String());
            AddColumn("dbo.StaffCheckInAndOutReports", "Department", c => c.String());
        }
    }
}
