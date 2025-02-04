

namespace EcommerceAPI.Application.Abstractions.Token
{
	public interface ITokenHandler
	{
		DTOs.Token CreateAccesToken(int second);
	}
}
