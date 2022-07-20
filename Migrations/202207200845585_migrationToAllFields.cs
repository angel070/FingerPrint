namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationToAllFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Holidays", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.LeaveTypes", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.LeaveTypes", "ShortType", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "Staff_id", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "Phone_no", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "Email", c => c.String());
            AlterColumn("dbo.Staffs", "Phone_no", c => c.String());
            AlterColumn("dbo.Staffs", "LastName", c => c.String());
            AlterColumn("dbo.Staffs", "FirstName", c => c.String());
            AlterColumn("dbo.Staffs", "Staff_id", c => c.String());
            AlterColumn("dbo.LeaveTypes", "ShortType", c => c.String());
            AlterColumn("dbo.LeaveTypes", "Type", c => c.String());
            AlterColumn("dbo.Holidays", "Name", c => c.String());
            AlterColumn("dbo.Departments", "Name", c => c.String());
            AlterColumn("dbo.Branches", "Name", c => c.String());
        }
    }
}
