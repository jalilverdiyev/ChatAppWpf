using System.Windows.Input;
using ChatApp.Application.Services;
using ChatApp.Wpf.Commands;
using ChatApp.Wpf.Services;

namespace ChatApp.Wpf.ViewModels;

public class LoginViewModel : BaseViewModel
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

	public ICommand LoginCommand { get; set; }
	public ICommand AuthSwitchCommand { get; set; }

	public LoginViewModel(IAuthService authService, INavigationService navigationService, IHubService hubService)
	{
		LoginCommand = new LoginCommand(this, authService, navigationService, hubService);
		AuthSwitchCommand = new AuthSwitchCommand(navigationService);
	}
}
