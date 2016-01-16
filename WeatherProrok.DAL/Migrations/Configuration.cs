namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WeatherProrok.DAL.WeatherContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WeatherProrok.DAL.WeatherContext context)
        {
            context.CloudityDict.AddOrUpdate(
                c => c.Name,
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "Ясно" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "Малооблачно" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "Облачно" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "Пасмурно" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "Неизвестно" }
                );

            context.PrecipitationDict.AddOrUpdate(
                p => p.Name,
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Нет данных" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Слабый дождь" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Дождь" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Сильный дождь" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Слабый снег" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Снег" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Сильный снег" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "Другое" }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
