using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Persistence.Contexts;
using F = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Persistence.Repositories
{
	public class FileWriteRepository : WriteRepository<F.File>, IFileWriteRepository
	{
		public FileWriteRepository(EcommerceAPIDbContext context) : base(context)
		{
		}
	}
}
