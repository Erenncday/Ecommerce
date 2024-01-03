using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		readonly private IProductReadRepository _productReadRepository;
		readonly private IProductWriteRepository _productWriteRepository;

		public TestController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
		}

		//[HttpGet]
		//public async Task Get()
		//{
		//	await _productWriteRepository.AddRangeAsync(new()
		//	{
		//		new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock=100},
		//		new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreatedDate = DateTime.UtcNow, Stock=200},
		//		new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreatedDate = DateTime.UtcNow, Stock=300},
		//	});

		//	var count = await _productWriteRepository.SaveAsync();
		//}

		[HttpGet]
		public async Task Get()
		{
			//Product p = await _productReadRepository.GetByIdAsync("2dc23820-5a44-4d26-85a6-6e2ca8793e86");
			//p.Name = "Telefon";
			//await _productWriteRepository.SaveAsync();

			Product prod = await _productReadRepository.GetByIdAsync("2dc23820-5a44-4d26-85a6-6e2ca8793e86", false);
			prod.Name = "Eren";
			await _productWriteRepository.SaveAsync();


			//Product pr = await _productReadRepository.GetByIdAsync("7158ba44-5152-4df1-9ab6-14909c9d9c9d");
			//pr.Name = "Bilgisayar";
			//await _productWriteRepository.SaveAsync();


		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			Product product = await _productReadRepository.GetByIdAsync(id);
			return Ok(product);
		}
	}
}
