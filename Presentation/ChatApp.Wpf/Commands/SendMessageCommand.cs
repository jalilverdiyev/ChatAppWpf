using System.Windows.Input;
using ChatApp.Application.Services;
using ChatApp.Wpf.Services;
using ChatApp.Wpf.ViewModels;

namespace ChatApp.Wpf.Commands;

public class SendMessageCommand(
		MessageInputViewModel messageInputViewModel,
		IAuthService authService,
		IHubService hubService) : ICommand
{
	public bool CanExecute(object? parameter)
	{
		var currentUserId = authService.CurrentUser?.Id;
		var selectProfile = messageInputViewModel.NavigationVm.SelectedProfile;

		return !(currentUserId is null || selectProfile is null);
	}

	public async void Execute(object? parameter)
	{
		var currentUserId = authService.CurrentUser?.Id;
		var selectProfile = messageInputViewModel.NavigationVm.SelectedProfile;

		if (currentUserId is null || selectProfile is null)
			return;

		await hubService.SendMessageAsync(currentUserId.Value, Guid.Parse(selectProfile.UserId),
				selectProfile.ConnId,
				messageInputViewModel.Text);
		messageInputViewModel.Text = "Type your Message";
	}

	public event EventHandler? CanExecuteChanged;
}
