using ChatApp.Application.Repositories;
using ChatApp.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Persistence;

public static class RegisterServices
{
	public static void RegisterPersistanceServices(this IServiceCollection services)
	{
		//TODO: Use App.conf for connection string
		services.AddDbContext<ChatAppDbContext>();

		services.AddScoped<IAppUserRepository, AppUserRepository>();
		services.AddScoped<IChatRepository, ChatRepository>();
		services.AddScoped<IMessageRepository, MessageRepository>();
	}
}
