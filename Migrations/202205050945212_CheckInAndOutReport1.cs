namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckInAndOutReport1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "Department", c => c.Int(nullable: false));
            AddColumn("dbo.StaffCheckInAndOutReports", "Branch", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffCheckInAndOutReports", "Branch");
            DropColumn("dbo.StaffCheckInAndOutReports", "Department");
        }
    }
}
