using ChatApp.Application.Repositories;
using ChatApp.Domain.Entities;

namespace ChatApp.Persistence.Repositories;

public class AppUserRepository(ChatAppDbContext chatAppDbContext) : Repository<AppUser>(chatAppDbContext), IAppUserRepository
{
}
