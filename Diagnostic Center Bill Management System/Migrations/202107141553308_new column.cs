namespace Diagnostic_Center_Bill_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestMasters", "Total", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestMasters", "Total");
        }
    }
}
