namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTimeInAndOutSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeInAndOuts", "WorkingHours", c => c.Int(nullable: false));
            AddColumn("dbo.TimeInAndOuts", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.TimeInAndOuts", "BranchId", c => c.Int(nullable: false));
            CreateIndex("dbo.TimeInAndOuts", "DepartmentId");
            CreateIndex("dbo.TimeInAndOuts", "BranchId");
            AddForeignKey("dbo.TimeInAndOuts", "BranchId", "dbo.Branches", "Id", cascadeDelete: false);
            AddForeignKey("dbo.TimeInAndOuts", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeInAndOuts", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.TimeInAndOuts", "BranchId", "dbo.Branches");
            DropIndex("dbo.TimeInAndOuts", new[] { "BranchId" });
            DropIndex("dbo.TimeInAndOuts", new[] { "DepartmentId" });
            DropColumn("dbo.TimeInAndOuts", "BranchId");
            DropColumn("dbo.TimeInAndOuts", "DepartmentId");
            DropColumn("dbo.TimeInAndOuts", "WorkingHours");
        }
    }
}
