namespace Diagnostic_Center_Bill_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestMasterId = c.Int(nullable: false),
                        TestSetupId = c.Int(nullable: false),
                        UserId = c.String(),
                        EntryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequestMasters", t => t.RequestMasterId, cascadeDelete: true)
                .ForeignKey("dbo.TestSetups", t => t.TestSetupId, cascadeDelete: true)
                .Index(t => t.RequestMasterId)
                .Index(t => t.TestSetupId);
            
            CreateTable(
                "dbo.RequestMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        UserId = c.String(),
                        EntryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestDetails", "TestSetupId", "dbo.TestSetups");
            DropForeignKey("dbo.RequestDetails", "RequestMasterId", "dbo.RequestMasters");
            DropIndex("dbo.RequestDetails", new[] { "TestSetupId" });
            DropIndex("dbo.RequestDetails", new[] { "RequestMasterId" });
            DropTable("dbo.RequestMasters");
            DropTable("dbo.RequestDetails");
        }
    }
}
