namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFactWeather1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FactWeathers", "Cloudity", c => c.Int(nullable: false));
            AlterColumn("dbo.FactWeathers", "Precipitations", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FactWeathers", "Precipitations", c => c.String());
            AlterColumn("dbo.FactWeathers", "Cloudity", c => c.String());
        }
    }
}
