using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Persistence.Contexts;
using F = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Persistence.Repositories
{
	public class FileReadRepository : ReadRepository<F.File>, IFileReadRepository
	{
		public FileReadRepository(EcommerceAPIDbContext context) : base(context)
		{
		}
	}
}
