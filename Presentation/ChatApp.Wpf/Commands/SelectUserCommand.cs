using System.Windows.Input;
using ChatApp.Wpf.ViewModels;

namespace ChatApp.Wpf.Commands;

public class SelectUserCommand(ProfileViewModel profileViewModel) : ICommand
{
	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public void Execute(object? parameter)
	{
		profileViewModel.DisplayMessages();
		profileViewModel.UpdateSelectedProfile();
	}

	public event EventHandler? CanExecuteChanged;
}
