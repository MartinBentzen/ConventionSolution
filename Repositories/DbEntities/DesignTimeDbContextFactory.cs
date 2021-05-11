using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Repositories.DbEntities
{
    /***
    * This class is needed for running DB migrations locally
    */
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MarvelConventionDbContext>
    {
        public DesignTimeDbContextFactory()
        {
        }

        public MarvelConventionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarvelConventionDbContext>();
            optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=MarvelConvention;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new MarvelConventionDbContext(optionsBuilder.Options);
        }
    }
}
