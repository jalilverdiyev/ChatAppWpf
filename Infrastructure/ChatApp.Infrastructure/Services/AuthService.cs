using ChatApp.Application.Dtos;
using ChatApp.Application.Exceptions;
using ChatApp.Application.Repositories;
using ChatApp.Application.Services;
using ChatApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Infrastructure.Services;

public class AuthService(IAppUserRepository userRepository, IPasswordHasher<AppUser> passwordHasher) : IAuthService
{
	public AppUser? CurrentUser { get; set; }

	public async Task<AppUser?> LoginUserAsync(string username, string password)
	{
		var user = await userRepository.FilterFirstAsync(u => u.Username == username);

		if (user is null)
		{
			CurrentUser = null;
			return null;
		}

		var res = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

		if (res == PasswordVerificationResult.Success)
		{
			CurrentUser = user;
			return user;
		}

		CurrentUser = null;
		return null;
	}

	public async Task<bool> RegisterAsync(RegisterDto dto)
	{
		var user = await userRepository.FilterFirstAsync(u => u.Username == dto.Username);

		if (user is not null)
			throw new AuthException("User exists");

		user = new AppUser()
		{
				Id = Guid.NewGuid(),
				Username = dto.Username,
				ProfilePhoto = dto.ProfilePhoto,
				CreatedDate = DateTime.UtcNow
		};

		user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

		return await userRepository.AddAsync(user);
	}
}
