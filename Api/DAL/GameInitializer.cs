using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Api.Entities;

namespace Api.DAL
{
    public class GameInitializer : DropCreateDatabaseIfModelChanges<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            var animal1 = new Animal { Name = "Panda", HappyLevel = 50, HappyLevelChange = 1, HungryLevel = 50, HungryLevelChange = 4, LastAction = DateTime.UtcNow };
            var animal2 = new Animal { Name = "Cat", HappyLevel = 50, HappyLevelChange = 2, HungryLevel = 50, HungryLevelChange = 3, LastAction = DateTime.UtcNow };
            var animal3 = new Animal { Name = "Dragon", HappyLevel = 50, HappyLevelChange = 3, HungryLevel = 50, HungryLevelChange = 2, LastAction = DateTime.UtcNow };
            var animal4 = new Animal { Name = "Fish", HappyLevel = 50, HappyLevelChange = 4, HungryLevel = 50, HungryLevelChange = 1, LastAction = DateTime.UtcNow };

            var users = new List<User>
            {
                new User {Name = "Lucifier", Animals = new List<Animal> { animal1, animal2 } },
                new User {Name = "Maze", Animals = new List<Animal> { animal3, animal4} }
            };



            users.ForEach(u => context.Users.AddOrUpdate(u));
            context.SaveChanges();
        }
    }
}
