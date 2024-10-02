using System.Windows.Input;
using ChatApp.Application.Services;
using ChatApp.Wpf.Commands;
using ChatApp.Wpf.Services;

namespace ChatApp.Wpf.ViewModels;

public class RegisterViewModel : BaseViewModel
{
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

	private string _password;

	public string Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged();
		}
	}

	public ICommand RegisterCommand { get; set; }
	public ICommand ProfilePhotoCommand { get; set; }
	public ICommand AuthSwitchCommand { get; set; }

	public RegisterViewModel(IAuthService authService, INavigationService navigationService)
	{
		RegisterCommand = new RegisterCommand(this, authService, navigationService);
		ProfilePhotoCommand = new ProfilePhotoCommand(this);
		AuthSwitchCommand = new AuthSwitchCommand(navigationService);
	}
}
