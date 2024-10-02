using System.Windows;
using ChatApp.Wpf.ViewModels;
using ChatApp.Wpf.Views;

namespace ChatApp.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow(MainViewModel viewModel)
	{
		viewModel.Navigation.NavigateTo<LoginViewModel>();
		DataContext = viewModel;

		InitializeComponent();
	}
}
