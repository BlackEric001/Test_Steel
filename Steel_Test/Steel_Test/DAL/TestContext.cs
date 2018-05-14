using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Steel_Test.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steel_Test.DAL
{
    public class TestContext : DbContext
    {
        public TestContext() : base("TestContext")
        {
        }
        
        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.PositionID)
                .WillCascadeOnDelete(false);


        }

    }
}