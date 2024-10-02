using System.Windows.Forms;
using System.Windows.Input;
using ChatApp.Application.Dtos;
using ChatApp.Application.Services;
using ChatApp.Wpf.Services;
using ChatApp.Wpf.ViewModels;

namespace ChatApp.Wpf.Commands;

public class RegisterCommand(
		RegisterViewModel registerViewModel,
		IAuthService authService,
		INavigationService navigationService) : ICommand
{
	public RegisterViewModel RegisterViewModel { get; set; } = registerViewModel;

	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public async void Execute(object? parameter)
	{
		var dto = new RegisterDto()
		{
				Username = registerViewModel.Username,
				ProfilePhoto = registerViewModel.ProfilePhoto,
				Password = registerViewModel.Password
		};

		var res = await authService.RegisterAsync(dto);

		if (res)
		{
			MessageBox.Show("Successfully registered");
			navigationService.NavigateTo<LoginViewModel>();
		}
		else
			MessageBox.Show("There were some problems");
	}

	public event EventHandler? CanExecuteChanged;
}
