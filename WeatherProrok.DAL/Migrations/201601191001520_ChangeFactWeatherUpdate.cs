namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFactWeatherUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FactWeathers", "Updated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FactWeathers", "Updated", c => c.DateTime(nullable: false));
        }
    }
}
