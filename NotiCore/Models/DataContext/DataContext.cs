using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
