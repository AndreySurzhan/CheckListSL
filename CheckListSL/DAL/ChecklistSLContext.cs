using CheckListSL.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace CheckListSL.DAL
{
    public class ChecklistSLContext : DbContext
    {
        // Your context has been configured to use a 'mylocaldb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CheckListSL.DAL.ChecklistSLContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'mylocaldb' 
        // connection string in the application configuration file.
        public ChecklistSLContext()
            : base("name=mylocaldb")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Translation> Translations { get; set; }
    }
}