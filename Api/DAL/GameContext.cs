//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Api.Entities;

//namespace Api.DAL
//{
//    public class GameContext : DbContext
//    {
//        public GameContext() : base("GameContext") { }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Animal> Animals { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//        }
//    }
//}
