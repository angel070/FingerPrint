namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTableLogo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        FilePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logoes");
        }
    }
}
