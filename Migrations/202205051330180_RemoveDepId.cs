namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDepId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Departments", "Dep_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "Dep_Id", c => c.Int(nullable: false));
        }
    }
}
