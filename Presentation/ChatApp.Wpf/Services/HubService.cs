using ChatApp.Application.Dtos;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Wpf.Services;

public class HubService : IHubService
{
	private readonly HubConnection _hubConnection;

	public event UpdateUsersHandler OnUsersUpdatedAsync;
	public event UpdateMessagesHandler OnMessagesUpdatedAsync;

	public HubService()
	{
		_hubConnection = new HubConnectionBuilder()
				.WithUrl("https://localhost:7293/chat")
				.WithAutomaticReconnect()
				.Build();

		_hubConnection.On("NewMessage", async (NewMessageDto msg) =>
		{
			await OnMessagesUpdatedAsync!(msg);
		});

		_hubConnection.On("UpdateContacts", async (List<UserDto> newUsers) =>
		{
			await OnUsersUpdatedAsync!(newUsers);
		});
	}

	public async Task ConnectAsync(Guid userId)
	{
		await _hubConnection.StartAsync();
		await _hubConnection.SendAsync("SetOnline", userId);
	}

	public async Task<List<UserDto>> GetOnlinesAsync()
	{
		var onlines = await _hubConnection.InvokeAsync<List<UserDto>>("GetOnlineUsers");

		return onlines;
	}

	public async Task SendMessageAsync(Guid userId, Guid toId, string toConnectionId, string message)
	{
		await _hubConnection.SendAsync("SendMessage", userId, toId, toConnectionId, message);
	}
}
