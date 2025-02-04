using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.User;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using AU = EcommerceAPI.Domain.Entities.Identity;



namespace EcommerceAPI.Persistence.Services
{
	public class UserService : IUserService
	{
		readonly UserManager<AU.AppUser> _userManager;

		public UserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserResponse> CreateAsync(CreateUser model)
		{
			IdentityResult result = await _userManager.CreateAsync(new()
			{
				Id = Guid.NewGuid().ToString(),
				NameSurname = model.NameSurname,
				UserName = model.UserName,
				Email = model.Email,

			}, model.Password);

			CreateUserResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
				response.Message = "Kullanıcı kaydı başarıyla oluşturuldu!";
			else
				foreach (var error in result.Errors)
					response.Message += $"{error.Code} - {error.Description}\n";

			return response;
		}
	}
}
