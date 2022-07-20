namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyStaffIdInCheckReport : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "StaffId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "StaffId", c => c.Int(nullable: false));
        }
    }
}
