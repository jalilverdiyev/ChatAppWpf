using System.Collections.ObjectModel;

namespace ChatApp.Wpf.ViewModels;

public class NavigationViewModel : BaseViewModel
{
	private ObservableCollection<ProfileViewModel> _profiles;

	public ObservableCollection<ProfileViewModel> Profiles
	{
		get => _profiles;
		set
		{
			_profiles = value;
			OnPropertyChanged();
		}
	}

	public ProfileViewModel? SelectedProfile { get; set; }

}
