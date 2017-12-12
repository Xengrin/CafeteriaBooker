namespace CafeteriaBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "Name");
        }
    }
}
