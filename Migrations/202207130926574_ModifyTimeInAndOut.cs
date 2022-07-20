namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTimeInAndOut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeInAndOuts", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeInAndOuts", "CreatedDate");
        }
    }
}
