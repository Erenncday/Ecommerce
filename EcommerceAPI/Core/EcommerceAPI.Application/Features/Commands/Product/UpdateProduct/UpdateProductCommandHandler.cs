﻿using EcommerceAPI.Application.Repositories;
using MediatR;
using P = EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
	{
		readonly IProductReadRepository _productReadRepository;
		readonly IProductWriteRepository _productWriteRepository;

		public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
		}

		public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			P.Product product = await _productReadRepository.GetByIdAsync(request.Id);

			product.Stock = request.Stock;
			product.Name = request.Name;
			product.Price = request.Price;

			await _productWriteRepository.SaveAsync();

			return new();
		}
	}
}
