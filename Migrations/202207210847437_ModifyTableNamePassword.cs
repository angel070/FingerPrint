namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTableNamePassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
        }
    }
}
