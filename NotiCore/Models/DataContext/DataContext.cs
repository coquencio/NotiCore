using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotiCore.API.Models.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SourceSubscription> SourceSubscriptions{ get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<TopicSubscription> TopicSubscriptions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FileStream openStream = File.OpenRead("Infraestructure/Seeds/SeedSources.json");
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var SourcesSeed = JsonSerializer.DeserializeAsync<Source[]>(openStream, options).GetAwaiter().GetResult();

            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.UserName})
                .IsUnique(true);

            modelBuilder.Entity<Subscriber>()
                .HasIndex(p => new { p.Email })
                .IsUnique(true);

            modelBuilder.Entity<Source>()
               .HasIndex(p => new { p.Url })
               .IsUnique(true);   

            modelBuilder.Entity<Article>()
                .HasIndex(p => new { p.Url, p.SourceId, p.TopicId });

            modelBuilder.Entity<Language>().HasData(
            new Language { LanguageId = 1, Description = "English", Abbreviation = "EN", IsActive = true },
            new Language { LanguageId = 2, Description = "Spanish", Abbreviation = "ES", IsActive = true }
            );

            modelBuilder.Entity<Source>()
                .HasData(SourcesSeed);


            modelBuilder.Entity<Topic>().HasData(
                new Topic { TopicId = 1, Description = "Tech", IsActive = true },
                new Topic { TopicId = 2, Description = "News", IsActive = true },
                new Topic { TopicId = 3, Description = "Business", IsActive = true },
                new Topic { TopicId = 4, Description = "Science", IsActive = true },
                new Topic { TopicId = 5, Description = "Finance", IsActive = true },
                new Topic { TopicId = 6, Description = "Food", IsActive = true },
                new Topic { TopicId = 7, Description = "Politics", IsActive = true },
                new Topic { TopicId = 8, Description = "Economics", IsActive = true },
                new Topic { TopicId = 9, Description = "Travel", IsActive = true },
                new Topic { TopicId = 10, Description = "Entertainment", IsActive = true },
                new Topic { TopicId = 11, Description = "Music", IsActive = true },
                new Topic { TopicId = 12, Description = "Sport", IsActive = true },
                new Topic { TopicId = 13, Description = "World", IsActive = true }
                );

        }
    }
}
