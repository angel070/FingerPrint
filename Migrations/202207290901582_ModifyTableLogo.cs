namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTableLogo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logoes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logoes", "Name");
        }
    }
}
