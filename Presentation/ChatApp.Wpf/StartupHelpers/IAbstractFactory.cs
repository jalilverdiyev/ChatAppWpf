namespace ChatApp.Wpf.StartupHelpers;

public interface IAbstractFactory<T>
{
	T Create();
}
