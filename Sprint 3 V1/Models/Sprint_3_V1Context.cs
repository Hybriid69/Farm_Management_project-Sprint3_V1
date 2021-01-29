using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class Sprint_3_V1Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Sprint_3_V1Context() : base("name=Sprint_3_V1Context")
        {
        }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.CropInfo> CropInfoes { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.CropTask> CropTasks { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.TaskMaster> TaskMasters { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Group> Groups { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Position> Positions { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.EmpPos> EmpPos { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Farm> Farms { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.FarmEmployee> FarmEmployees { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Field> Fields { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Land> Lands { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.GroupTask> GroupTasks { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.PlantedTask> PlantedTasks { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Planted> Planteds { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Sale> Sales { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.Stock> Stocks { get; set; }

        public System.Data.Entity.DbSet<Sprint_3_V1.Models.StockOrder> StockOrders { get; set; }
        public System.Data.Entity.DbSet<Sprint_3_V1.Models.StocksImage> StocksImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
