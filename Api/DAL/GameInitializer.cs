//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Api.Entities;

//namespace Api.DAL
//{
//    public class GameInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GameContext>
//    {
//        protected override void Seed(GameContext context)
//        {
//            var users = new List<User>
//            {
//                new User {Name = "Lucifier"},
//                new User {Name = "Maze"},
//            };
//            users.ForEach(u=> context.Users.Add(u));
//            context.SaveChanges();
//        }
//    }
//}
