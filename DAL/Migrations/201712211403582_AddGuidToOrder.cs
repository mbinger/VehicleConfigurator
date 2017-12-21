namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Guid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Guid");
        }
    }
}
