using ChatApp.Application.Dtos;
using ChatApp.Application.Repositories;
using ChatApp.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.SignalR;

public class ChatHub : Hub
{
	private readonly IChatRepository _chatRepository;
	private readonly IMessageRepository _messageRepository;
	private readonly IAppUserRepository _appUserRepository;
	private static readonly Dictionary<Guid, string> Users = [];

	public ChatHub(IChatRepository chatRepository, IMessageRepository messageRepository,
			IAppUserRepository appUserRepository)
	{
		_chatRepository = chatRepository;
		_messageRepository = messageRepository;
		_appUserRepository = appUserRepository;

		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine("Initiated ChatHub");
		Console.ResetColor();
	}

	public async Task SetOnline(Guid userId)
	{
		Users[userId] = Context.ConnectionId;

		var userIds = Users
				.Select(u => u.Key)
				.ToList();
		var users = (await _appUserRepository
						.FilterAll(u => userIds.Contains(u.Id))
						.ToListAsync())
				.Select(u => new UserDto()
				{
						Id = u.Id,
						ConnId = Users[u.Id],
						Username = u.Username,
						ProfilePhoto = u.ProfilePhoto
				}).ToList();

		await Clients.All.SendAsync("UpdateContacts", users);
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var usr = Users.SingleOrDefault(u => u.Value == Context.ConnectionId);

		Users.Remove(usr.Key);

		var userIds = Users
				.Select(u => u.Key)
				.ToList();
		var users = (await _appUserRepository
						.FilterAll(u => userIds.Contains(u.Id))
						.ToListAsync())
				.Select(u => new UserDto()
				{
						Id = u.Id,
						ConnId = Users[u.Id],
						Username = u.Username,
						ProfilePhoto = u.ProfilePhoto
				}).ToList();

		await Clients.All.SendAsync("UpdateContacts", users);

		await base.OnDisconnectedAsync(exception);
	}

	public async Task<List<UserDto>> GetOnlineUsers()
	{
		var userIds = Users
				.Where(u => u.Value != Context.ConnectionId)
				.Select(u => u.Key)
				.ToList();
		var users = (await _appUserRepository
						.FilterAll(u => userIds.Contains(u.Id))
						.ToListAsync())
				.Select(u => new UserDto()
				{
						Id = u.Id,
						ConnId = Users[u.Id],
						Username = u.Username,
						ProfilePhoto = u.ProfilePhoto
				}).ToList();

		return users;
	}

	public async Task SendMessage(Guid userId, Guid toId, string connectionId, string message)
	{
		var user = await _appUserRepository.GetByIdAsync(userId);
		var toUser = await _appUserRepository.GetByIdAsync(toId);

		if (user is null || toUser is null)
			return;

		var chat = await _chatRepository.FilterFirstAsync(c =>
				c.Chatters.Any(ch => ch.Id == userId) && c.Chatters.Any(ch => ch.Id == toId));

		var msg = new Message()
		{
				Id = Guid.NewGuid(),
				Text = message,
				CreatedDate = DateTime.UtcNow,
				OwnerId = userId
		};

		if (chat is null)
		{
			chat = new Chat()
			{
					Id = Guid.NewGuid(),
					Chatters = [user, toUser],
					Messages = [msg],
					CreatedDate = DateTime.UtcNow
			};

			await _chatRepository.AddAsync(chat);
		}
		else
		{
			msg.ChatId = chat.Id;
			await _messageRepository.AddAsync(msg);
		}

		await Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());
		await Groups.AddToGroupAsync(connectionId, chat.Id.ToString());

		var newMsg = new NewMessageDto()
		{
				Text = msg.Text,
				ToId = toId,
				Owner = user,
				CreatedDate = msg.CreatedDate
		};
		await Clients.Group(chat.Id.ToString()).SendAsync("NewMessage", newMsg);
	}
}
