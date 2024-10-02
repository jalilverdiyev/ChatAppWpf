using System.ComponentModel;
using ChatApp.Wpf.ViewModels;

namespace ChatApp.Wpf.Services;

public interface INavigationService
{
	BaseViewModel CurrentViewModel { get; }

	void NavigateTo<T>() where T: BaseViewModel;
}
