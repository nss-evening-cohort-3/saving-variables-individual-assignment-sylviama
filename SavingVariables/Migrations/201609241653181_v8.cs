namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.charValues",
                c => new
                    {
                        charId = c.Int(nullable: false, identity: true),
                        charName = c.String(nullable: false),
                        value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.charId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.charValues");
        }
    }
}
