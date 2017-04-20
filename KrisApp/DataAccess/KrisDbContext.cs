﻿using KrisApp.DataModel.Article;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Logs;
using KrisApp.DataModel.Users;
using KrisApp.DataModel.Work;
using System.Data.Entity;

namespace KrisApp.DataAccess
{
    public class KrisDbContext : DbContext
    {
        // TODO: przenieść do oddzielnej biblioteki
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<SkillType> SkillTypes { get; set; }
        public DbSet<PositionType> PositionTypes { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<AppLog> AppLogs { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        public KrisDbContext() : base(Properties.Settings.Default.csDB)
        {
            Database.SetInitializer<KrisDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }
    }
}