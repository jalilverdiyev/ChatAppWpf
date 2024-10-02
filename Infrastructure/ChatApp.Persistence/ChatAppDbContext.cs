using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence;

public class ChatAppDbContext : DbContext
{
	public DbSet<AppUser> Users { get; set; }
	public DbSet<Chat> Chats { get; set; }
	public DbSet<Message> Messages { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=.\\LOCAL;Database=ChatAppDb;Integrated Security=true; TrustServerCertificate=true");
		base.OnConfiguring(optionsBuilder);
	}
}
