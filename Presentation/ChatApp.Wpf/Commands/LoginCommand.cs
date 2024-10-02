using System.Windows.Forms;
using System.Windows.Input;
using ChatApp.Application.Services;
using ChatApp.Wpf.Services;
using ChatApp.Wpf.ViewModels;

namespace ChatApp.Wpf.Commands;

public class LoginCommand(
		LoginViewModel loginViewModel,
		IAuthService authService,
		INavigationService navigationService,
		IHubService hubService) : ICommand
{
	private readonly LoginViewModel _loginViewModel = loginViewModel;

	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public async void Execute(object? parameter)
	{
		var res = await authService.LoginUserAsync(loginViewModel.Username, loginViewModel.Password);

		MessageBox.Show(res is null ? "Failed" : "Successful login");

		if(res is null)
			return;

		await hubService.ConnectAsync(res.Id);
		navigationService.NavigateTo<ChatViewModel>();
	}

	public event EventHandler? CanExecuteChanged;
}
