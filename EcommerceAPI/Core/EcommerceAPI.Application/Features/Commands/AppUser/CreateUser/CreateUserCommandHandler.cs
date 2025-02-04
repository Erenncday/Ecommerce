using AU = EcommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.User;


namespace EcommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		readonly IUserService _userService;

		public CreateUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{

			 CreateUserResponse response = await _userService.CreateAsync(new()
			{
				UserName = request.UserName,
				Email = request.Email,
				NameSurname = request.NameSurname,
				Password = request.Password,
				Password2 = request.Password2
			});

			return new()
			{
				Message = response.Message,
				Succeeded = response.Succeeded,
			};
			

			//throw new UserCreateFailedException();

		}
	}
}
