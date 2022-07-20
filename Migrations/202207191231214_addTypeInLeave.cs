namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTypeInLeave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "LeaveTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Leaves", "LeaveName", c => c.String(nullable: false));
            AlterColumn("dbo.Leaves", "StaffId", c => c.String(nullable: false));
            CreateIndex("dbo.Leaves", "LeaveTypeId");
            AddForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes");
            DropIndex("dbo.Leaves", new[] { "LeaveTypeId" });
            AlterColumn("dbo.Leaves", "StaffId", c => c.String());
            AlterColumn("dbo.Leaves", "LeaveName", c => c.String());
            DropColumn("dbo.Leaves", "LeaveTypeId");
        }
    }
}
