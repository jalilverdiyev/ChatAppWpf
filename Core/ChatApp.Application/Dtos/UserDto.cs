namespace ChatApp.Application.Dtos;

public class UserDto
{
	public Guid Id { get; set; }
	public string ConnId { get; set; }
	public string ProfilePhoto { get; set; }
	public string Username { get; set; }
	public int UnreadCount { get; set; }
}
