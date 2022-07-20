namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyworkingHoursInTimeInAndOut : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeInAndOuts", "WorkingHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeInAndOuts", "WorkingHours", c => c.Int(nullable: false));
        }
    }
}
