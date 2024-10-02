using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChatApp.Wpf.ViewModels;

namespace ChatApp.Wpf.Services;

public class NavigationService : INotifyPropertyChanged, INavigationService
{
	private readonly Func<Type, BaseViewModel> _viewModelFactor;
	public event PropertyChangedEventHandler? PropertyChanged;

	private BaseViewModel _currentViewModel;

	public BaseViewModel CurrentViewModel
	{
		get => _currentViewModel;
		set
		{
			_currentViewModel = value;
			OnPropertyChanged();
		}
	}

	public NavigationService(Func<Type, BaseViewModel> viewModelFactor)
	{
		_viewModelFactor = viewModelFactor;
	}

	public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
	{
		BaseViewModel viewModel = _viewModelFactor.Invoke(typeof(TViewModel));
		CurrentViewModel = viewModel;
	}

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
