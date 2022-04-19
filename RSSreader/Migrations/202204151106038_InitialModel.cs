namespace RSSreader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuId = c.String(),
                        Title = c.String(),
                        Link = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        FeedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.FeedId, cascadeDelete: true)
                .Index(t => t.FeedId);
            
            CreateTable(
                "dbo.Feeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "FeedId", "dbo.Feeds");
            DropIndex("dbo.Articles", new[] { "FeedId" });
            DropTable("dbo.Feeds");
            DropTable("dbo.Articles");
        }
    }
}
