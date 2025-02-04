using EcommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using EcommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
		{
			CreateUserCommandResponse respose = await _mediator.Send(createUserCommandRequest);
			return Ok(respose);
		}
	}
}
