namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaffCheckinAndOutchanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StaffCheckInAndOutReports", "status", c => c.Boolean(nullable: false));
        }
    }
}
