using System.Windows.Input;
using ChatApp.Wpf.Commands;

namespace ChatApp.Wpf.ViewModels;

public class ProfileViewModel : BaseViewModel
{
	private string _profilePhoto;

	public string ProfilePhoto
	{
		get => _profilePhoto;
		set
		{
			_profilePhoto = value;
			OnPropertyChanged();
		}
	}

	private string _userId;

	public string UserId
	{
		get => _userId;
		set
		{
			_userId = value;
			OnPropertyChanged();
		}
	}

	private string _connId;

	public string ConnId
	{
		get => _connId;
		set
		{
			_connId = value;
			OnPropertyChanged();
		}
	}

	private int _unreadCount;

	public int UnreadCount
	{
		get => _unreadCount;
		set
		{
			_unreadCount = value;
			OnPropertyChanged();
		}
	}

	public ICommand SelectUserCommand { get; set; }
	public Action DisplayMessages { get; set; }
	public Action UpdateSelectedProfile { get; set; }

	public ProfileViewModel()
	{
		SelectUserCommand = new SelectUserCommand(this);
	}
}
