using System.ComponentModel.DataAnnotations.Schema;
using ChatApp.Domain.Entities.Common;

namespace ChatApp.Domain.Entities;

public class Message : BaseEntity
{
	public string Text { get; set; }
	[ForeignKey(nameof(Chat))] public Guid ChatId { get; set; }
	public Chat Chat { get; set; }
	[ForeignKey(nameof(Owner))] public Guid OwnerId { get; set; }
	public AppUser Owner { get; set; }
	public bool IsRead { get; set; }
}
