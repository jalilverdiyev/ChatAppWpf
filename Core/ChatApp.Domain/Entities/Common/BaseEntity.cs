namespace ChatApp.Domain.Entities.Common;

public abstract class BaseEntity
{
	public Guid Id { get; set; }
	public DateTime? UpdatedDate { get; set; }
	public DateTime CreatedDate { get; set; }
}
