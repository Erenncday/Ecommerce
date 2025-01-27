using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Features.Commands.ImageFiles.RemoveProductImage
{
	public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
	{
		readonly IProductReadRepository _productReadRepository;
		readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

		public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_productImageFileWriteRepository = productImageFileWriteRepository;
		}

		public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
		{
			P.Product? product = await _productReadRepository.Table.Include(p => p.productImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

			P.ProductImageFile? productImageFile = product?.productImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

			if (productImageFile != null)
				product?.productImageFiles.Remove(productImageFile);

			await _productImageFileWriteRepository.SaveAsync();
			return new();
		}
	}
}
