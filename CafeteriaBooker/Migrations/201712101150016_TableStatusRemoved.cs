namespace CafeteriaBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableStatusRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tables", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tables", "Status", c => c.Boolean(nullable: false));
        }
    }
}
