using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.Abstractions.Token;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AU = EcommerceAPI.Domain.Entities.Identity.AppUser;

namespace EcommerceAPI.Persistence.Services
{
	public class AuthService : IAuthService
	{
		readonly UserManager<AU> _userManager;
		readonly SignInManager<AU> _signInManager;
		readonly ITokenHandler _tokenHandler;
		readonly IUserService _userService;

		public AuthService(UserManager<AU> userManager, SignInManager<AU> signInManager, ITokenHandler tokenHandler, IUserService userService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
			_userService = userService;
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
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5);

				return token;
			}

			//return new LoginUserErrorCommandResponse()
			//{
			//	Message = "Kullanıcı Adı veya Şifre hatalı!"
			//};

			throw new AuthenticationErrorException();
		}

		public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
		{
			AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
			{
				Token token = _tokenHandler.CreateAccesToken(15);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
				return token;
			}
			else
				throw new NotFoundUserException();
		}
	}
}
