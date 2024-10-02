using System.Windows;
using ChatApp.Infrastructure;
using ChatApp.Persistence;
using ChatApp.Wpf.Services;
using ChatApp.Wpf.StartupHelpers;
using ChatApp.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
	private readonly IHost? _host;

	public App()
	{
		_host = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					services.RegisterViews();
					services.RegisterViewModels();
					services.AddSingleton<MainWindow>();
					services.AddSingleton<IHubService, HubService>();
					services.RegisterPersistanceServices();
					services.RegisterInfrastructureServices();
					services.AddSingleton<INavigationService, NavigationService>();
					services.AddSingleton<Func<Type, BaseViewModel>>((provider =>
							type => (BaseViewModel)provider.GetRequiredService(type)));
				})
				.Build();
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		await _host!.StartAsync();

		var window = _host.Services.GetRequiredService<MainWindow>();
		window.Show();

		base.OnStartup(e);
	}

	protected override async void OnExit(ExitEventArgs e)
	{
		await _host!.StopAsync();
		base.OnExit(e);
	}
}
