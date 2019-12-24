namespace SaveTime.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Barbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WorkDayStart = c.DateTime(nullable: false),
                        WorkDayEnd = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                        Branch_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Branch_Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        StartWork = c.DateTime(nullable: false),
                        EndWork = c.DateTime(nullable: false),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Price = c.Double(nullable: false),
                        ApproximatelySpendTimeInMinutes = c.Double(nullable: false),
                        Barber_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Barbers", t => t.Barber_Id)
                .Index(t => t.Barber_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingTime = c.DateTime(nullable: false),
                        SpendTime = c.Time(nullable: false, precision: 7),
                        Barber_Id = c.Int(),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Barbers", t => t.Barber_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Barber_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.SystemAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemAdmins", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Records", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Records", "Barber_Id", "dbo.Barbers");
            DropForeignKey("dbo.Clients", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Services", "Barber_Id", "dbo.Barbers");
            DropForeignKey("dbo.Barbers", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.Branches", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Barbers", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.SystemAdmins", new[] { "Account_Id" });
            DropIndex("dbo.Records", new[] { "Client_Id" });
            DropIndex("dbo.Records", new[] { "Barber_Id" });
            DropIndex("dbo.Clients", new[] { "Account_Id" });
            DropIndex("dbo.Services", new[] { "Barber_Id" });
            DropIndex("dbo.Branches", new[] { "Company_Id" });
            DropIndex("dbo.Barbers", new[] { "Branch_Id" });
            DropIndex("dbo.Barbers", new[] { "Account_Id" });
            DropTable("dbo.SystemAdmins");
            DropTable("dbo.Records");
            DropTable("dbo.Clients");
            DropTable("dbo.Services");
            DropTable("dbo.Companies");
            DropTable("dbo.Branches");
            DropTable("dbo.Barbers");
            DropTable("dbo.Accounts");
        }
    }
}
