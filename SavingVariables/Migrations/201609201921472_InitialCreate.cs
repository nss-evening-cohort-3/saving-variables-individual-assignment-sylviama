namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.charValues", "charName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.charValues", "charName");
        }
    }
}
