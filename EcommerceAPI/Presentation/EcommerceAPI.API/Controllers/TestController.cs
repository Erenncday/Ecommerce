using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Application.RequestParameters;
using EcommerceAPI.Application.ViewModels.Products;
using EcommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

		#region Test Kodları

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

		//[HttpGet]
		//public async Task Get()
		//{
		//	//Product p = await _productReadRepository.GetByIdAsync("2dc23820-5a44-4d26-85a6-6e2ca8793e86");
		//	//p.Name = "Telefon";
		//	//await _productWriteRepository.SaveAsync();

		//	//Product prod = await _productReadRepository.GetByIdAsync("2dc23820-5a44-4d26-85a6-6e2ca8793e86", false);
		//	//prod.Name = "Eren";
		//	//await _productWriteRepository.SaveAsync();


		//	//Product pr = await _productReadRepository.GetByIdAsync("7158ba44-5152-4df1-9ab6-14909c9d9c9d");
		//	//pr.Name = "Bilgisayar";
		//	//await _productWriteRepository.SaveAsync();
		//}

		//[httpget]
		//public async task get()
		//{
		//	//var customerıd = guid.newguid();
		//	//await _customerwriterepository.addasync(new() { name = "eren", ıd = customerıd });

		//	//await _orderwriterepository.addasync(new() { description = "bla bla bla", address = "istanbul, maltepe", customerıd = customerıd });
		//	//await _orderwriterepository.addasync(new() { description = "bla bla bla", address = "kayseri, yahyalı", customerıd = customerıd });
		//	//await _orderwriterepository.saveasync();

		//	//order order = await _orderreadrepository.getbyıdasync("fc53d446-0179-4bd3-a58e-09f6203708e5");
		//	//order.address = "adana";
		//	//await _orderwriterepository.saveasync();
		//}

		//[HttpGet("{id}")]
		//public async Task<IActionResult> Get(string id)
		//{
		//	Product product = await _productReadRepository.GetByIdAsync(id);
		//	return Ok(product);
		//}

		#endregion

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery]Pagination pagination)
		{
			var totalCount = _productReadRepository.GetAll(false).Count();

			var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
			{
				p.Id,
				p.Name,
				p.Stock,
				p.Price,
				p.CreatedDate,
				p.UpdatedDate
			}).ToList();

			return Ok(new
			{
				totalCount,
				products
			});
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			return Ok(await _productReadRepository.GetByIdAsync(id, false));
		}

		[HttpPost]
		public async Task<IActionResult> Post(VM_Create_Product model)
		{
			await _productWriteRepository.AddAsync(new()
			{
				Name = model.Name,
				Price = model.Price,
				Stock = model.Stock
			});

			await _productWriteRepository.SaveAsync();

			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut]
		public async Task<IActionResult> Put(VM_Update_Product model)
		{
			Product product = await _productReadRepository.GetByIdAsync(model.Id);

			product.Stock = model.Stock;
			product.Name = model.Name;
			product.Price = model.Price;

			await _productWriteRepository.SaveAsync();

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _productWriteRepository.RemoveAsync(id);
			await _productWriteRepository.SaveAsync();
			return Ok();
		}
	}
}
