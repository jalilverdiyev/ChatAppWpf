using System.Windows.Input;
using ChatApp.Domain.Enums;
using ChatApp.Wpf.Services;
using ChatApp.Wpf.ViewModels;
using ChatApp.Wpf.Views;

namespace ChatApp.Wpf.Commands;

public class AuthSwitchCommand(INavigationService navService) : ICommand
{
	public bool CanExecute(object? parameter) => parameter is AuthSwitchType;

	public void Execute(object? parameter)
	{
		if(parameter is not AuthSwitchType switchType)
			return;

		switch (switchType)
		{
			case AuthSwitchType.Login:
				navService.NavigateTo<LoginViewModel>();
				break;
			case AuthSwitchType.Register:
				navService.NavigateTo<RegisterViewModel>();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public event EventHandler? CanExecuteChanged;
}
