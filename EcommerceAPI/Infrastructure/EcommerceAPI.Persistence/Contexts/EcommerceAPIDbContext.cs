﻿using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Common;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Contexts
{
	public class EcommerceAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
	{
		public EcommerceAPIDbContext(DbContextOptions options) : base(options)
		{

		}

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

		public DbSet<Domain.Entities.File> files { get; set; }
		public DbSet<ProductImageFile> productImageFiles { get; set; }
		public DbSet<InvoiceFile> InvoiceFiles { get; set; }


		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
				_ = data.State switch
				{
					EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
					EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
					_ => DateTime.UtcNow
				};

            }

            return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
