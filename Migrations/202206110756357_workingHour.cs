namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workingHour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkingHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        workingHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkingHours");
        }
    }
}
