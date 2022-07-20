namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewStaffs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeInAndOuts", "TimeIn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeInAndOuts", "TimeIn", c => c.Time(nullable: false, precision: 7));
        }
    }
}
