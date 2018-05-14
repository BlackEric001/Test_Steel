using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using Steel_Test.Models;

namespace Steel_Test.DAL
{
    public class TestInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TestContext>
    {
        protected override void Seed(TestContext context)
        {

            var position = new List<Position>
            {
                new Position{PositionName = "1st"},
                new Position{PositionName = "2nd"},
                new Position{PositionName = "3rd"},
                new Position{PositionName = "4th"}
            };

            position.ForEach(s => context.Positions.Add(s));
            context.SaveChanges();
            
            var users = new List<User>
            {
                new User{FIO="Иванов Иван Иванович", TableNumber = 1, Position = position.Single(i => i.PositionName == "1st"), Photo = new byte[] {0x20}},
                new User{FIO="Петров П. П.", TableNumber = 2, Position = position.Single(i => i.PositionName == "2nd"), Photo = new byte[] {0x20}},
                new User{FIO="Петров С. И.", TableNumber = 3, Position = position.Single(i => i.PositionName == "3rd"), Photo = new byte[] {0x20}},
                new User{FIO="Васильев Ваня", TableNumber = 4, Position = position.Single(i => i.PositionName == "3rd"), Photo = new byte[] {0x20}},
                new User{FIO="Сидоров Петя", TableNumber = 5, Position = position.Single(i => i.PositionName == "3rd"), Photo = new byte[] {0x20}},
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }

    }
}