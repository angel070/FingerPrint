namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staffTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "Fingerprint", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "Fingerprint", c => c.String(nullable: false));
        }
    }
}
