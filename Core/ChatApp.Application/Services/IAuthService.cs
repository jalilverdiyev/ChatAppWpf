using ChatApp.Application.Dtos;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.Services;

public interface IAuthService
{
	AppUser? CurrentUser { get; set; }

	Task<AppUser?> LoginUserAsync(string username, string password);

	Task<bool> RegisterAsync(RegisterDto dto);
}
