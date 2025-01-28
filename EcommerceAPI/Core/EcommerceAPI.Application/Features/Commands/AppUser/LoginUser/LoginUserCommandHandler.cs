using EcommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AU = EcommerceAPI.Domain.Entities.Identity.AppUser;

namespace EcommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
		readonly UserManager<AU> _userManager;
		readonly SignInManager<AU> _signInManager;

		public LoginUserCommandHandler(UserManager<AU> userManager, SignInManager<AU> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
			AU user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
			if (user == null)
				user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

			if (user == null)
				throw new NotFoundUserException();

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                
            }

			return new();
        }
	}
}
