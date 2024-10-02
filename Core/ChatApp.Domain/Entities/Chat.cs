using ChatApp.Domain.Entities.Common;

namespace ChatApp.Domain.Entities;

public class Chat : BaseEntity
{
	public ICollection<AppUser> Chatters { get; set; }
	public ICollection<Message> Messages { get; set; }
}
