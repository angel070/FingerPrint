namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allpending : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
