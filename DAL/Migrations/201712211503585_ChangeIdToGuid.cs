namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIdToGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderAdditionalEquipmentItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderAdditionalEquipmentItems", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.OrderAdditionalEquipmentItems", "OrderId");
            AddColumn("dbo.OrderAdditionalEquipmentItems", "OrderId", c => c.Guid(nullable: false));
            DropColumn("dbo.Orders", "Id");
            AddColumn("dbo.Orders", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Orders", "Id");
            CreateIndex("dbo.OrderAdditionalEquipmentItems", "OrderId");
            AddForeignKey("dbo.OrderAdditionalEquipmentItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderAdditionalEquipmentItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderAdditionalEquipmentItems", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "Id");
            AddColumn("dbo.Orders", "Id", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.OrderAdditionalEquipmentItems", "OrderId");
            AddColumn("dbo.OrderAdditionalEquipmentItems", "OrderId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Orders", "Id");
            CreateIndex("dbo.OrderAdditionalEquipmentItems", "OrderId");
            AddForeignKey("dbo.OrderAdditionalEquipmentItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
