namespace FingerPrint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: false)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Holidays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaffCheckInAndOutReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.String(),
                        firstName = c.String(),
                        LastName = c.String(),
                        Department = c.Int(nullable: false),
                        DepartmentStr = c.String(),
                        Branch = c.Int(nullable: false),
                        BranchStr = c.String(),
                        Date = c.DateTime(nullable: false),
                        TimeIn = c.DateTime(),
                        TimeOut = c.DateTime(),
                        status = c.String(),
                        TotalStaffHours = c.Time(precision: 7),
                        PercentageHours = c.Decimal(precision: 18, scale: 2),
                        OverTimeHours = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.TimeInAndOuts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Days = c.Int(nullable: false),
                        TimeIn = c.DateTime(nullable: false),
                        TimeOut = c.DateTime(nullable: false),
                        WorkingHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepartmentId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserRolesId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: false)
                .ForeignKey("dbo.UserRoles", t => t.UserRolesId, cascadeDelete: false)
                .Index(t => t.UserRolesId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.WorkingHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        workingHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRolesId", "dbo.UserRoles");
            DropForeignKey("dbo.Users", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.TimeInAndOuts", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.TimeInAndOuts", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Staffs", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Staffs", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Staffs", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Departments", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Branches", "CountryId", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "BranchId" });
            DropIndex("dbo.Users", new[] { "UserRolesId" });
            DropIndex("dbo.TimeInAndOuts", new[] { "BranchId" });
            DropIndex("dbo.TimeInAndOuts", new[] { "DepartmentId" });
            DropIndex("dbo.Staffs", new[] { "DepartmentId" });
            DropIndex("dbo.Staffs", new[] { "BranchId" });
            DropIndex("dbo.Staffs", new[] { "CountryId" });
            DropIndex("dbo.Departments", new[] { "BranchId" });
            DropIndex("dbo.Branches", new[] { "CountryId" });
            DropTable("dbo.WorkingHours");
            DropTable("dbo.Users");
            DropTable("dbo.TimeInAndOuts");
            DropTable("dbo.Staffs");
            DropTable("dbo.StaffCheckInAndOutReports");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Holidays");
            DropTable("dbo.Events");
            DropTable("dbo.Departments");
            DropTable("dbo.Countries");
            DropTable("dbo.Branches");
        }
    }
}
