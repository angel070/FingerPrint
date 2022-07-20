namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "TimeOut", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "TimeOut", c => c.DateTime(nullable: false));
        }
    }
}
