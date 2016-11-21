using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Api.Entities;

namespace Api.DAL
{
    public class GameContext : DbContext
    {
        
        public GameContext() : base("GameContext")
        {
            Database.SetInitializer(new GameInitializer());
        }

        public GameContext(DbConnection connection) : base(connection, true)
        {
            Database.SetInitializer(new GameInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
