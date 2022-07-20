namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnToCheckReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffCheckInAndOutReports", "DepartmentStr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffCheckInAndOutReports", "DepartmentStr");
        }
    }
}
