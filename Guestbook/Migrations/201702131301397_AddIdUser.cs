namespace Guestbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageBody = c.String(),
                        MessageDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Id_User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id_User_Id)
                .Index(t => t.Id_User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Id_User_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Id_User_Id" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
