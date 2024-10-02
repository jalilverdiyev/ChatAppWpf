using ChatApp.Application.Repositories;
using ChatApp.Domain.Entities;

namespace ChatApp.Persistence.Repositories;

public class MessageRepository(ChatAppDbContext chatAppDbContext) : Repository<Message>(chatAppDbContext), IMessageRepository
{
}
