using System;
using System.Data.Entity.Migrations;

namespace FingerPrint.Migrations
{
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeInAndOuts", "Days", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeInAndOuts", "Days");
        }
    }
}
