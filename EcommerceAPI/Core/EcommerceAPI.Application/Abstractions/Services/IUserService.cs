﻿using EcommerceAPI.Application.DTOs.User;
using EcommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task<CreateUserResponse> CreateAsync(CreateUser model);
		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
	}
}
