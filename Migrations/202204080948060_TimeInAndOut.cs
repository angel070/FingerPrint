namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeInAndOut : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeInAndOuts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeIn = c.DateTime(nullable: false),
                        TimeOut = c.DateTime(nullable: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeInAndOuts");
        }
    }
}
