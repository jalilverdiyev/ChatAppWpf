using System.Windows.Input;
using ChatApp.Application.Services;
using ChatApp.Wpf.Commands;
using ChatApp.Wpf.Services;

namespace ChatApp.Wpf.ViewModels;

public class MessageInputViewModel : BaseViewModel
{
	private string _text;

	public string Text
	{
		get => _text;
		set
		{
			_text = value;
			OnPropertyChanged();
		}
	}

	public NavigationViewModel NavigationVm { get; set; }
	public ICommand SendMessageCommand { get; set; }

	public MessageInputViewModel(IAuthService authService, IHubService hubService)
	{
		Text = "Type your Message";
		SendMessageCommand = new SendMessageCommand(this, authService, hubService);
	}
}
