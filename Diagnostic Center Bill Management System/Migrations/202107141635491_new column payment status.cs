namespace Diagnostic_Center_Bill_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolumnpaymentstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestMasters", "BillPaymentStatus", c => c.String());
            AddColumn("dbo.RequestMasters", "PayDueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestMasters", "PayDueDate");
            DropColumn("dbo.RequestMasters", "BillPaymentStatus");
        }
    }
}
