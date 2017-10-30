namespace CallTrackingTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverideManytToManyCascadeOnDeleteConvention : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReportsCalls", "ReportsId", "dbo.Reports");
            DropForeignKey("dbo.ReportsCalls", "CallId", "dbo.Calls");
            AddForeignKey("dbo.ReportsCalls", "ReportsId", "dbo.Reports", "Id");
            AddForeignKey("dbo.ReportsCalls", "CallId", "dbo.Calls", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportsCalls", "CallId", "dbo.Calls");
            DropForeignKey("dbo.ReportsCalls", "ReportsId", "dbo.Reports");
            AddForeignKey("dbo.ReportsCalls", "CallId", "dbo.Calls", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReportsCalls", "ReportsId", "dbo.Reports", "Id", cascadeDelete: true);
        }
    }
}
