namespace Sprint_3_V1.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sprint_3_V1.Models.Sprint_3_V1Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sprint_3_V1.Models.Sprint_3_V1Context context)
        {
            Account admin = new Account
            {
                UserName = "Farmerbrown1",
                Password = "Farmerbrown1",
                ConfirmPassword = "Farmerbrown1",
                Role = "Admin",
                Disabled = false,
                Type = "Admin",
                AccountID = 1,
                Customer = null,
                Employee = null

            };
            context.Accounts.Add(admin);
            context.SaveChanges();

            IList<Position> positions = new List<Position>();

            positions.Add(new Position()
            {
                PositionID = 1,
                Name = "Manager",
                Description = "Manage Farm",
                BaseSalary = 1234.0,
                Disabled = false,
                EmpPos = null
            });
            positions.Add(new Position()
            {
                PositionID = 2,
                Name = "Human resources",
                Description = "Manage Farmers",
                BaseSalary = 1234.0,
                Disabled = false,
                EmpPos = null
            });
            positions.Add(new Position()
            {
                PositionID = 3,
                Name = "Worker",
                Description = "Field work",
                BaseSalary = 1230.0,
                Disabled = false,
                EmpPos = null
            });
            positions.Add(new Position()
            {
                PositionID = 4,
                Name = "Foreman",
                Description = "Manage Farmers",
                BaseSalary = 123.0,
                Disabled = false,
                EmpPos = null
            });
            positions.Add(new Position()
            {
                PositionID = 5,
                Name = "Clerk",
                Description = "Manage Admin",
                BaseSalary = 120.0,
                Disabled = false,
                EmpPos = null
            });
            positions.Add(new Position()
            {
                PositionID = 6,
                Name = "Delivery",
                Description = "Deliver",
                BaseSalary = 120.0,
                Disabled = false,
                EmpPos = null
            });

            context.Positions.AddRange(positions);
            context.SaveChanges();


            CropInfo crop = new CropInfo
            {
                CropID = 1,
                Name = "Cabbage",
                Description = "Cabbage farm",
                Season = "Summer",
                Spacing = 2,
                AverageYield = 100,
                IrrigationInterval = 3,
                GrowthTime = 60,
                LifeExpect = 30,
                Disabled = false
            };
            context.CropInfoes.Add(crop);
            context.SaveChanges();

            Stock stock = new Stock
            {
                StockID = 1,
                CurQuantity = 100,
                HarQuantity = 100,
                Harvested = DateTime.Now,
                Expiery = DateTime.Now.AddDays(30),
                ExpFlag = false,
                Price = 100,
                Disabled = false,
                CropID = 1
            };
            context.Stocks.Add(stock);
            context.SaveChanges();

            //StocksImage stocksimage = new StocksImage
            //{
            //    StockImageID = 1,
            //    StockID = 1,
            //    ImageName = "Cabbage",
            //    StockImage = ImageToArray(),

            //};
            //context.StocksImages.Add(stocksimage);
            //context.SaveChanges();

            base.Seed(context);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}