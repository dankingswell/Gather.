namespace CallTrackingTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReportsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        Data = c.String(),
                        FileName = c.String(),
                        RequestedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffMembers", t => t.StaffId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.ReportsCalls",
                c => new
                    {
                        ReportsId = c.Int(nullable: false),
                        CallId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReportsId, t.CallId })
                .ForeignKey("dbo.Reports", t => t.ReportsId, cascadeDelete: false)
                .ForeignKey("dbo.Calls", t => t.CallId, cascadeDelete: false)
                .Index(t => t.ReportsId)
                .Index(t => t.CallId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "StaffId", "dbo.StaffMembers");
            DropForeignKey("dbo.ReportsCalls", "CallId");
            DropForeignKey("dbo.ReportsCalls", "ReportsId");
            DropIndex("dbo.ReportsCalls", new[] { "CallId" });
            DropIndex("dbo.ReportsCalls", new[] { "ReportsId" });
            DropIndex("dbo.Reports", new[] { "StaffId" });
            DropTable("dbo.ReportsCalls");
            DropTable("dbo.Reports");
        }
    }
}
