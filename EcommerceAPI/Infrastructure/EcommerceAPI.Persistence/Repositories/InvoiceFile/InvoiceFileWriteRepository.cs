using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories
{
	public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
	{
		public InvoiceFileWriteRepository(EcommerceAPIDbContext context) : base(context)
		{
		}
	}
}
