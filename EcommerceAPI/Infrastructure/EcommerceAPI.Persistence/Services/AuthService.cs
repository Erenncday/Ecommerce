using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.Abstractions.Token;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using Microsoft.AspNetCore.Identity;

using AU = EcommerceAPI.Domain.Entities.Identity.AppUser;

namespace EcommerceAPI.Persistence.Services
{
	public class AuthService : IAuthService
	{
		readonly UserManager<AU> _userManager;
		readonly SignInManager<AU> _signInManager;
		readonly ITokenHandler _tokenHandler;

		public AuthService(UserManager<AU> userManager, SignInManager<AU> signInManager, ITokenHandler tokenHandler)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
		}

		public async Task<Token> LoginAsync(string UsernameOrEmail, string Password, int accessTokenLifeTime)
		{
			AU user = await _userManager.FindByNameAsync(UsernameOrEmail);
			if (user == null)
				user = await _userManager.FindByEmailAsync(UsernameOrEmail);
			if (user == null)
				throw new NotFoundUserException();

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, Password, false);
			if (result.Succeeded)
			{
				Token token = _tokenHandler.CreateAccesToken(accessTokenLifeTime);

				return token;
			}

			//return new LoginUserErrorCommandResponse()
			//{
			//	Message = "Kullanıcı Adı veya Şifre hatalı!"
			//};

			throw new AuthenticationErrorException();
		}
	}
}
