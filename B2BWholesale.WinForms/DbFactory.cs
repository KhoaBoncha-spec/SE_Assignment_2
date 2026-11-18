using System.Configuration;
using B2BWholesale.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.WinForms
{
    public static class DbFactory
    {
        public static B2BContext CreateContext()
        {
            string connStr = ConfigurationManager.ConnectionStrings["B2BDatabase"]?.ConnectionString
                ?? throw new InvalidOperationException("Missing B2BDatabase in App.config");
            var options = new DbContextOptionsBuilder<B2BContext>()
                .UseSqlServer(connStr)
                .Options;
            return new B2BContext(options);
        }
    }
}