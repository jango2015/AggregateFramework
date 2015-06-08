using System.Data.Entity;
using App.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests
{
    public abstract class EntityFrameworkIntegrationTest
    {
        protected static AppContext DbContext;
        
        public static void RefreshDatabase()
        {
            DbContext = new AppContext("TestDatabase");
            Database.Delete("TestDatabase");
            DbContext.Database.CreateIfNotExists();
        }
    }
}
