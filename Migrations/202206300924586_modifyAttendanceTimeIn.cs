namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyAttendanceTimeIn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "TimeIn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "TimeIn", c => c.DateTime(nullable: false));
        }
    }
}
