namespace Guestbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIdUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "Id_User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Id_User", c => c.Int());
        }
    }
}
