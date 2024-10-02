using ChatApp.Wpf.Controls;
using ChatApp.Wpf.ViewModels;
using ChatApp.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Wpf.StartupHelpers;

public static class RegisterServices
{
	public static void RegisterViews(this IServiceCollection services)
	{
		services.RegisterView<RegisterView>();
		services.RegisterView<LoginView>();
		services.RegisterView<ChatView>();
		services.RegisterView<MessageInputControl>();
	}

	public static void RegisterViewModels(this IServiceCollection services)
	{
		services.RegisterViewModel<MainViewModel>();
		services.RegisterViewModel<RegisterViewModel>();
		services.RegisterViewModel<LoginViewModel>();
		services.RegisterViewModel<ChatViewModel>();
		services.RegisterViewModel<MessageInputViewModel>();
	}

	private static void RegisterView<TView>(this IServiceCollection services) where TView : class
	{
		services.AddTransient<TView>();
		services.AddSingleton<Func<TView>>(x => () => x.GetService<TView>()!);
		services.AddSingleton<IAbstractFactory<TView>, AbstractFactory<TView>>();
	}

	private static void RegisterViewModel<TViewModel>(this IServiceCollection services) where TViewModel : class
	{
		services.AddTransient<TViewModel>();
		services.AddSingleton<Func<TViewModel>>(x => () => x.GetService<TViewModel>()!);
		services.AddSingleton<IAbstractFactory<TViewModel>, AbstractFactory<TViewModel>>();
	}
}
