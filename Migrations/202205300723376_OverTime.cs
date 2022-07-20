namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "OverTimeHours", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffCheckInAndOutReports", "OverTimeHours");
        }
    }
}
