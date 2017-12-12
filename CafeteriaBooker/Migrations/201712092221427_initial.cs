namespace CafeteriaBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TableID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tables", t => t.TableID, cascadeDelete: true)
                .Index(t => t.TableID);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Smoke = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "TableID", "dbo.Tables");
            DropIndex("dbo.Reservations", new[] { "TableID" });
            DropTable("dbo.Tables");
            DropTable("dbo.Reservations");
        }
    }
}
