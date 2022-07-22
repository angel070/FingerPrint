namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTableUsers1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
