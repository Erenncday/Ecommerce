using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories
{
	public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
	{
		public InvoiceFileReadRepository(EcommerceAPIDbContext context) : base(context)
		{
		}
	}
}
