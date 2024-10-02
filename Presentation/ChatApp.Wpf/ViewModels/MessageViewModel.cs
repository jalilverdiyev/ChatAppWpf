namespace ChatApp.Wpf.ViewModels;

public class MessageViewModel : BaseViewModel
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

	private string _message;

	public string Message
	{
		get => _message;
		set
		{
			_message = value;
			OnPropertyChanged();
		}
	}

	private string _username;

	public string Username
	{
		get => _username;
		set
		{
			_username = value;
			OnPropertyChanged();
		}
	}
}
