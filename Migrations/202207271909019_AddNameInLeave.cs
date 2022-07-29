namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameInLeave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "Name");
        }
    }
}
