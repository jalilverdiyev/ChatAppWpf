using ChatApp.Wpf.Services;

namespace ChatApp.Wpf.ViewModels;

public class MainViewModel : BaseViewModel
{
	private INavigationService _navigationService;

	public INavigationService Navigation
	{
		get => _navigationService;
		set
		{
			_navigationService = value;
			OnPropertyChanged();
		}
	}

	public MainViewModel(INavigationService navigationService)
	{
		_navigationService = navigationService;
	}
}
