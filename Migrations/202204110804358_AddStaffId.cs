namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "StaffId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffCheckInAndOutReports", "StaffId");
        }
    }
}
