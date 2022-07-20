namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyLeaveTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Leaves", "LeaveName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "LeaveName", c => c.String(nullable: false));
        }
    }
}
