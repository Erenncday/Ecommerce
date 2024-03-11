using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories
{
	public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
	{
		public ProductImageFileReadRepository(EcommerceAPIDbContext context) : base(context)
		{
		}
	}
}
