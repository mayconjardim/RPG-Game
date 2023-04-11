using Microsoft.EntityFrameworkCore;
using RPG_Game.Models;

namespace RPG_Game.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Fireball", Damage = 30},
                new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
                new Skill { Id = 3, Name = "Blizzard", Damage = 19 },
                new Skill { Id = 4, Name = "Air Bazook", Damage = 40 },
                new Skill { Id = 5, Name = "Dragon Slayer", Damage = 52 }
                );
        }

        public DbSet<Character> Characters { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Skill> Skills { get; set; }



    }
}
