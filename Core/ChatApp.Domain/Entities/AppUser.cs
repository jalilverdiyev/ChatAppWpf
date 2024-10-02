using ChatApp.Domain.Entities.Common;

namespace ChatApp.Domain.Entities;

public class AppUser : BaseEntity
{
	public string Username { get; set; }
	public string ProfilePhoto { get; set; }
	public string PasswordHash { get; set; }
	public ICollection<Chat> Chats { get; set; }
}
