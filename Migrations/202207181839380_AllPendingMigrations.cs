namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllPendingMigrations : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TimeInAndOuts", "CreatedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeInAndOuts", "CreatedDate", c => c.DateTime());
        }
    }
}
