using EcommerceAPI.Application.ViewModels.Products;
using FluentValidation;



namespace EcommerceAPI.Application.Validators.Products
{
	public class CreateProductValidator : AbstractValidator<VM_Create_Product>
	{
		public CreateProductValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.NotNull()
					.WithMessage("Lütfen ürün adı giriniz.")
				.MaximumLength(150)
					.WithMessage("Ürün adı 150 karakterden fazla olamaz!")
				.MinimumLength(2)
					.WithMessage("Ürün adı 2 karakterden az olamaz!");

			RuleFor(s => s.Stock)
				.NotEmpty()
				.NotNull()
					.WithMessage("Lütfen stok adedi giriniz.")
				.Must(s => s >= 0)
					.WithMessage("stok adedi 0'dan az olamaz!");

			RuleFor(p => p.Price)
				.NotEmpty()
				.NotNull()
					.WithMessage("Lütfen fiyat bilgisi giriniz.")
				.Must(s => s >= 0)
					.WithMessage("stok fiyat bilgisi 0'dan az olamaz!");
		}
	}
}
