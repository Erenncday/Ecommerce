using EcommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EcommerceAPIDbContext>
	{
		public EcommerceAPIDbContext CreateDbContext(string[] args)
		{
			DbContextOptionsBuilder<EcommerceAPIDbContext> dbContextOptionsBuilder = new();
			dbContextOptionsBuilder.UseNpgsql(Configurations.ConnectionString);

			return new(dbContextOptionsBuilder.Options);
		}
	}
}
