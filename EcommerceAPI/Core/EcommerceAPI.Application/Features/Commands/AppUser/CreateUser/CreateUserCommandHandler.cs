using AU = EcommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EcommerceAPI.Application.Exceptions;


namespace EcommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		readonly UserManager<AU.AppUser> _userManager;

		public CreateUserCommandHandler(UserManager<AU.AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			IdentityResult result =  await _userManager.CreateAsync(new()
			{
				Id = Guid.NewGuid().ToString(),
				NameSurname = request.NameSurname,
				UserName = request.UserName,
				Email = request.Email,

			}, request.Password);

			CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
				response.Message = "Kullanıcı kaydı başarıyla oluşturuldu!";
			else
				foreach (var error in result.Errors)
					response.Message += $"{error.Code} - {error.Description}\n";

			return response;

			//throw new UserCreateFailedException();

		}
	}
}
