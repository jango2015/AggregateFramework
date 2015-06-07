using System;
using System.Data.Entity.Migrations;
using App.Core.Products;
using App.Core.ReferenceData;

namespace App.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppContext context)
        {            
            context.Products.AddOrUpdate(
                p => p.Id,
                new ProductState
                {
                    Id = new Guid("dbee54ff-dd97-4545-87a6-da2ba7cb638e"),
                    Name = "Chicken Livers",
                    Description = "Some good",
                    Price = 999,
                    AvailablityArea = Locations.DtJuneau,
                    Discontinued = false
                },
                new ProductState
                {
                    Id = new Guid("7d1e24f4-83d5-4cd8-b402-fe7a748efff1"),
                    Name = "Hog Maw",
                    Description = "Even better!",
                    Price = 498,
                    AvailablityArea = Locations.DtJuneau,
                    Discontinued = false
                },
                new ProductState
                {
                    Id = new Guid("097e1049-b0db-4ae1-a141-03f4c2921de4"),
                    Name = "Bud Lite",
                    Description = "Head for the mountains. No seriously, get out of here.",
                    Price = 1499,
                    AvailablityArea = Locations.DtJuneau,
                    Discontinued = false
                },
                new ProductState
                {
                    Id = new Guid("0044963b-1572-43ac-a6dd-dd881a31095b"),
                    Name = "Baby Wipes",
                    Description = "I don't have a baby. Don't judge me.",
                    Price = 749,
                    AvailablityArea = Locations.DtJuneau,
                    Discontinued = false
                },
                new ProductState
                {
                    Id = new Guid("8cd3cdb7-27d8-4505-b530-cb6489f267e0"),
                    Name = "Cucumber",
                    Description = "Garden fresh, Organic",
                    Price = 179,
                    AvailablityArea = Locations.DtJuneau,
                    Discontinued = false
                });
        }
    }
}
