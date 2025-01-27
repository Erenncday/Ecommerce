using EcommerceAPI.Application.Abstractions.Storage;
using EcommerceAPI.Application.Features.Commands.ImageFiles.RemoveProductImage;
using EcommerceAPI.Application.Features.Commands.ImageFiles.UploadProductImage;
using EcommerceAPI.Application.Features.Commands.Product.CreateProduct;
using EcommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using EcommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using EcommerceAPI.Application.Features.Queries.ImageFiles.GetProductImage;
using EcommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using EcommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Application.RequestParameters;
using EcommerceAPI.Application.ViewModels.Products;
using EcommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		readonly private IProductReadRepository _productReadRepository;
		readonly private IProductWriteRepository _productWriteRepository;

		readonly private IWebHostEnvironment _webHostEnvironment;


		readonly IFileReadRepository _fileReadRepository;
		readonly IFileWriteRepository _fileWriteRepository;

		readonly IProductImageFileReadRepository _productImageFileReadRepository;
		readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

		readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
		readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

		readonly IStorageService _storageService;
		readonly IConfiguration _configuration;

		readonly IMediator _mediator;


		public TestController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
			_webHostEnvironment = webHostEnvironment;
			_fileReadRepository = fileReadRepository;
			_fileWriteRepository = fileWriteRepository;
			_productImageFileReadRepository = productImageFileReadRepository;
			_productImageFileWriteRepository = productImageFileWriteRepository;
			_invoiceFileReadRepository = invoiceFileReadRepository;
			_invoiceFileWriteRepository = invoiceFileWriteRepository;
			_storageService = storageService;
			_configuration = configuration;
			_mediator = mediator;
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

		//[HttpPost]
		//public async Task<IActionResult> Post(VM_Create_Product model)
		//{
		//	await _productWriteRepository.AddAsync(new()
		//	{
		//		Name = model.Name,
		//		Price = model.Price,
		//		Stock = model.Stock
		//	});

		//	await _productWriteRepository.SaveAsync();

		//	return StatusCode((int)HttpStatusCode.Created);
		//}

		#endregion

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
		{
			//var totalCount = _productReadRepository.GetAll(false).Count();

			//var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
			//{
			//	p.Id,
			//	p.Name,
			//	p.Stock,
			//	p.Price,
			//	p.CreatedDate,
			//	p.UpdatedDate
			//}).ToList();

			//return Ok(new
			//{
			//	totalCount,
			//	products
			//});

			GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
		{
			GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
		{
			CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);

			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
		{
			UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);

			return Ok(response);
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
		{
			RemoveProductCommandResponse reponse = await _mediator.Send(removeProductCommandRequest);
			return Ok();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest uploadProductImageCommandRequest)
		{
			#region Test
			////var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);
			//var datas = await _storageService.UploadAsync("files", Request.Form.Files);

			////var datas =	await _fileService.UploadAsync("resource/Files", Request.Form.Files);
			//_productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
			//{
			//	FileName = d.fileName,
			//	Path = d.pathOrContainerName,
			//	Storage = _storageService.StorageName
			//}).ToList());

			////await _productImageFileWriteRepository.SaveAsync();

			////_fileWriteRepository.AddRangeAsync(datas.Select(d => new EcommerceAPI.Domain.Entities.File()
			////{
			////	FileName = d.fileName,
			////	Path = d.path
			////}).ToList());

			//await _fileWriteRepository.SaveAsync();

			//return Ok();
			#endregion

			uploadProductImageCommandRequest.Files = Request.Form.Files;
			UploadProductImageCommandResponse reponse = await _mediator.Send(uploadProductImageCommandRequest);
			return Ok();

		}

		[HttpGet("[action]/{Id}")]
		public async Task<IActionResult> GetProductImages([FromRoute]GetProductImageQueryRequest getProductImageQueryRequest)
		{
			List<GetProductImageQueryResponse> response = await _mediator.Send(getProductImageQueryRequest);
			return Ok(response);
		}


		[HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest,[FromQuery]string imageId )
		{
			removeProductImageCommandRequest.ImageId = imageId;
			RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
			return Ok();
		}
    }
}
