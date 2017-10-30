namespace CallTrackingTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CallInitiated = c.DateTime(nullable: false),
                        CallReason = c.String(),
                        CallNotes = c.String(),
                        ClientId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.StaffMembers", t => t.StaffId)
                .Index(t => t.ClientId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffFirstName = c.String(nullable: false),
                        StaffLastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calls", "StaffId", "dbo.StaffMembers");
            DropForeignKey("dbo.Calls", "ClientId", "dbo.Clients");
            DropIndex("dbo.Calls", new[] { "StaffId" });
            DropIndex("dbo.Calls", new[] { "ClientId" });
            DropTable("dbo.StaffMembers");
            DropTable("dbo.Clients");
            DropTable("dbo.Calls");
        }
    }
}
