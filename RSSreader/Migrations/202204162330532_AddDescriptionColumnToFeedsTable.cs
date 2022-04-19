namespace RSSreader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionColumnToFeedsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feeds", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feeds", "Description");
        }
    }
}
