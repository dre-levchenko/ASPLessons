namespace Gallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlbumModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        Album_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Images", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Images", new[] { "Album_Id" });
            DropIndex("dbo.Albums", new[] { "Owner_Id" });
            DropTable("dbo.Images");
            DropTable("dbo.Albums");
        }
    }
}
