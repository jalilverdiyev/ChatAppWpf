using ChatApp.Application.Repositories;
using ChatApp.Domain.Entities;

namespace ChatApp.Persistence.Repositories;

public class ChatRepository(ChatAppDbContext chatAppDbContext) : Repository<Chat>(chatAppDbContext), IChatRepository
{
}
