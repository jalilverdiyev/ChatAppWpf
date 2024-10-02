using ChatApp.Domain.Entities;

namespace ChatApp.Application.Dtos;

public class NewMessageDto
{
	public string Text { get; set; }
	public AppUser Owner { get; set; }
	public Guid ToId { get; set; }
	public DateTime CreatedDate { get; set; }
}
