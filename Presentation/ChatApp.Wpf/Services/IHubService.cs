using ChatApp.Application.Dtos;

namespace ChatApp.Wpf.Services;

public delegate Task UpdateUsersHandler(List<UserDto> users);
public delegate Task UpdateMessagesHandler(NewMessageDto newMessage);
public interface IHubService
{
	event UpdateUsersHandler OnUsersUpdatedAsync;
	event UpdateMessagesHandler OnMessagesUpdatedAsync;
	Task ConnectAsync(Guid userId);
	Task<List<UserDto>> GetOnlinesAsync();
	Task SendMessageAsync(Guid userId, Guid toId, string toConnectionId, string message);
}
