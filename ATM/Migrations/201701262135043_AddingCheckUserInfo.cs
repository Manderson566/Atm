namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCheckUserInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logins", "Account_Id", "dbo.UserInfoes");
            DropIndex("dbo.Logins", new[] { "Account_Id" });
            DropTable("dbo.Logins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Logins", "Account_Id");
            AddForeignKey("dbo.Logins", "Account_Id", "dbo.UserInfoes", "Id");
        }
    }
}
