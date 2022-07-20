namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCheckInAndOut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "totalHours", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffCheckInAndOutReports", "totalHours");
        }
    }
}
