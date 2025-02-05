

using EcommerceAPI.Domain.Entities.Identity;

namespace EcommerceAPI.Application.Abstractions.Token
{
	public interface ITokenHandler
	{
		DTOs.Token CreateAccesToken(int second, AppUser appuser);
		string CreateRefreshToken();
	}
}
