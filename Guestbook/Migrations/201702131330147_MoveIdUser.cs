namespace Guestbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveIdUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Id_User_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Id_User_Id" });
            RenameColumn(table: "dbo.Messages", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Messages", "Id_User", c => c.Int());
            AlterColumn("dbo.Messages", "MessageBody", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "User_Id", c => c.Int());
            CreateIndex("dbo.Messages", "User_Id");
            AddForeignKey("dbo.Messages", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Users", "Id_User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Id_User_Id", c => c.Int());
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            AlterColumn("dbo.Messages", "User_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "MessageBody", c => c.String());
            DropColumn("dbo.Messages", "Id_User");
            RenameColumn(table: "dbo.Messages", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.Users", "Id_User_Id");
            CreateIndex("dbo.Messages", "UserId");
            AddForeignKey("dbo.Messages", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "Id_User_Id", "dbo.Users", "Id");
        }
    }
}
