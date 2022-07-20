namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataToFingerprint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "Fingerprint", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "Fingerprint", c => c.String());
        }
    }
}
