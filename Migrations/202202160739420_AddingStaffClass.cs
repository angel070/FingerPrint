namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStaffClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Staff_id = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone_no = c.String(),
                        CountryId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Email = c.String(),
                        Fingerprint = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .Index(t => t.CountryId)
                .Index(t => t.BranchId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Staffs", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Staffs", "BranchId", "dbo.Branches");
            DropIndex("dbo.Staffs", new[] { "DepartmentId" });
            DropIndex("dbo.Staffs", new[] { "BranchId" });
            DropIndex("dbo.Staffs", new[] { "CountryId" });
            DropTable("dbo.Staffs");
        }
    }
}
