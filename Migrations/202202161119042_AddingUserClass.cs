namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserRolesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRolesId, cascadeDelete: false)
                .Index(t => t.UserRolesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRolesId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRolesId" });
            DropTable("dbo.Users");
        }
    }
}
