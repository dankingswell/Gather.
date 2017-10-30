namespace CallTrackingTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostCodeFieldsToClients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "PostCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "PostCode");
        }
    }
}
