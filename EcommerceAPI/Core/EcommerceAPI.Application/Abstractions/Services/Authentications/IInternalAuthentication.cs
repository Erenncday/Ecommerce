

namespace EcommerceAPI.Application.Abstractions.Services.Authentications
{
	public interface IInternalAuthentication
	{
		Task<DTOs.Token> LoginAsync(string UsernameOrEmail, string Password, int accessTokenLifeTime);
		Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);


	}
}
