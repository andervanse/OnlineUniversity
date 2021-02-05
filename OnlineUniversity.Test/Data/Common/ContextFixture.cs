using Microsoft.EntityFrameworkCore;
using OnlineUniversity.Data.EFCore;
using System;
using Xunit;

namespace OnlineUniversity.Test.Data
{
    public class ContextFixture : IDisposable
    {
       
        public ContextFixture()
        {
            DbContextOptionsBuilder<DataContext> optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineUniversity_db;Trusted_Connection=True;");
            optionsBuilder.UseInMemoryDatabase(databaseName: "OnlineUniversity_db");

            context = new DataContext(optionsBuilder.Options);
            DatabaseInitializer.Initialize(context);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public DataContext context { get; private set; }
    }

    [CollectionDefinition("Database collection")]
    public class ContextCollection : ICollectionFixture<ContextFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
