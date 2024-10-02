using System.Collections.ObjectModel;
using ChatApp.Application.Dtos;
using ChatApp.Application.Repositories;
using ChatApp.Application.Services;
using ChatApp.Wpf.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Wpf.ViewModels;

public class ChatViewModel : BaseViewModel
{
	private readonly IHubService _hubService;
	private readonly IAuthService _authService;
	private readonly IChatRepository _chatRepository;
	private readonly IMessageRepository _messageRepository;
	private ObservableCollection<MessageViewModel> _messages = [];

	public ObservableCollection<MessageViewModel> Messages
	{
		get => _messages;
		set
		{
			_messages = value;
			OnPropertyChanged();
		}
	}

	private NavigationViewModel _navigationVm;

	public NavigationViewModel NavigationVm
	{
		get => _navigationVm;
		set
		{
			_navigationVm = value;
			OnPropertyChanged();
		}
	}

	private MessageInputViewModel _messageInputVm;

	public MessageInputViewModel MessageInputVm
	{
		get => _messageInputVm;
		set
		{
			_messageInputVm = value;
			OnPropertyChanged();
		}
	}

	public ChatViewModel(IHubService hubService, IAuthService authService, IChatRepository chatRepository,
			IMessageRepository messageRepository)
	{
		NavigationVm = new NavigationViewModel();
		MessageInputVm = new MessageInputViewModel(authService, hubService)
		{
				Text = "Type your Message",
				NavigationVm = NavigationVm
		};

		_hubService = hubService;
		_authService = authService;
		_chatRepository = chatRepository;
		_messageRepository = messageRepository;

		_ = LoadUsersAsync();
		_hubService.OnUsersUpdatedAsync += UpdateUsersAsync(authService, chatRepository, messageRepository);
		_hubService.OnMessagesUpdatedAsync += UpdateMessagesAsync();
	}

	private UpdateMessagesHandler UpdateMessagesAsync()
	{
		return async msg =>
		{
			if (NavigationVm.SelectedProfile is null)
			{
				var profile = NavigationVm.Profiles.FirstOrDefault(p => p.UserId == msg.Owner.Id.ToString());
				profile!.UnreadCount++;
				await Task.CompletedTask;
				return;
			}

			if (msg.ToId.ToString() == NavigationVm.SelectedProfile.UserId ||
			    msg.Owner.Id.ToString() == NavigationVm.SelectedProfile.UserId)
			{
				var newMessages = Messages.Append(new MessageViewModel()
				{
						Message = msg.Text,
						ProfilePhoto = msg.Owner.ProfilePhoto,
						Username = $"{msg.Owner.Username} - {msg.CreatedDate:t}"
				});
				Messages = new ObservableCollection<MessageViewModel>(newMessages);
			}
			else
			{
				var profile = NavigationVm.Profiles.FirstOrDefault(p => p.UserId == msg.Owner.Id.ToString());
				profile!.UnreadCount++;
			}
			await Task.CompletedTask;
		};
	}

	private UpdateUsersHandler UpdateUsersAsync(IAuthService authService, IChatRepository chatRepository, IMessageRepository messageRepository)
	{
		return async (users) =>
		{
			await UpdateProfilesAsync(users);

			if (NavigationVm.SelectedProfile is null)
			{
				await Task.CompletedTask;
				return;
			}

			if (users.All(p => p.Id.ToString() != NavigationVm.SelectedProfile.UserId))
				Messages = [];
		};
	}

	private async Task ReadMessagesAsync()
	{
		var currentUserId = _authService.CurrentUser!.Id;
		var selectedUserId = Guid.Parse(NavigationVm.SelectedProfile!.UserId);
		var chat = await _chatRepository.FilterFirstAsync(ch =>
				ch.Chatters.Any(c => c.Id == currentUserId) &&
				ch.Chatters.Any(c => c.Id == selectedUserId));

		if(chat is null)
			return;

		await _messageRepository.FilterAll(m => m.ChatId == chat.Id && m.OwnerId == selectedUserId)
				.ExecuteUpdateAsync(m => m.SetProperty(p => p.IsRead, true));
		await LoadUsersAsync();
	}

	private async Task LoadMessagesAsync(Guid userId)
	{
		var currentUserId = _authService.CurrentUser!.Id;
		var chat = await _chatRepository.Table
				.Where(c => c.Chatters.Any(ch => ch.Id == userId) && c.Chatters.Any(ch => ch.Id == currentUserId))
				.Include(c => c.Messages)
				.ThenInclude(m => m.Owner)
				.FirstOrDefaultAsync();

		if (chat is null)
		{
			Messages = [];
			return;
		}

		var messages = chat.Messages
				.OrderBy(m => m.CreatedDate)
				.Select(m => new MessageViewModel()
				{
						Message = m.Text,
						ProfilePhoto = m.Owner.ProfilePhoto,
						Username = $"{m.Owner.Username} - {m.CreatedDate:t}"
				})
				.ToList();
		Messages = new ObservableCollection<MessageViewModel>(messages);
		NavigationVm.SelectedProfile!.UnreadCount = 0;
	}

	private async Task LoadUsersAsync()
	{
		var onlines = await _hubService.GetOnlinesAsync();
		await UpdateProfilesAsync(onlines);
	}

	private async Task UpdateProfilesAsync(List<UserDto> newUsers)
	{
		var loggedInUsr = _authService.CurrentUser!;

		var usrChats = _chatRepository.Table
				.Where(c => c.Chatters.Contains(loggedInUsr))
				.Select(c => c.Id);

		var unreads = await _messageRepository
				.FilterAll(m => m.OwnerId != loggedInUsr.Id && usrChats.Contains(m.ChatId) && !m.IsRead)
				.GroupBy(m => m.OwnerId)
				.Select(g => new
				{
						OwnerId = g.Key,
						Count = g.Count()
				})
				.ToDictionaryAsync(g => g.OwnerId);

		var profiles = newUsers.Select(o =>
		{
			unreads.TryGetValue(o.Id, out var unread);

			var pm = new ProfileViewModel()
			{
					UserId = o.Id.ToString(),
					ConnId = o.ConnId,
					ProfilePhoto = o.ProfilePhoto,
					UnreadCount = unread?.Count ?? 0,
					DisplayMessages = GetMessages,
			};
			pm.UpdateSelectedProfile = () => { NavigationVm.SelectedProfile = pm; };

			return pm;

			async void GetMessages()
			{
				await LoadMessagesAsync(o.Id);
				await ReadMessagesAsync();
			}
		});

		NavigationVm.Profiles = new ObservableCollection<ProfileViewModel>(profiles);
	}
}
