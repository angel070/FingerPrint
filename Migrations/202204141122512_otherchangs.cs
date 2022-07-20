namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otherchangs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaffCheckInAndOutReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        firstName = c.String(),
                        LastName = c.String(),
                        Date = c.DateTime(nullable: false),
                        TimeIn = c.DateTime(nullable: false),
                        TimeOut = c.DateTime(),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeInAndOuts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeIn = c.DateTime(nullable: false),
                        TimeOut = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Staffs", "FirstName", c => c.String());
            AlterColumn("dbo.Staffs", "LastName", c => c.String());
            AlterColumn("dbo.Staffs", "Phone_no", c => c.String());
            AlterColumn("dbo.Staffs", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "Phone_no", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "FirstName", c => c.String(nullable: false));
            DropTable("dbo.TimeInAndOuts");
            DropTable("dbo.StaffCheckInAndOutReports");
        }
    }
}
