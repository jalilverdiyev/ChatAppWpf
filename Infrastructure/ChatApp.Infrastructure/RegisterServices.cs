using ChatApp.Application.Services;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Infrastructure;

public static class RegisterServices
{
	public static void RegisterInfrastructureServices(this IServiceCollection services)
	{
		services.AddIdentityCore<AppUser>();
		services.AddSingleton<IAuthService, AuthService>();
	}
}
