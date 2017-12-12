namespace CafeteriaBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableSeatsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "Seats", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "Seats");
        }
    }
}
