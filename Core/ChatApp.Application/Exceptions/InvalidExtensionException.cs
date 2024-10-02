namespace ChatApp.Application.Exceptions;

public class InvalidExtensionException : Exception
{
	public InvalidExtensionException() : base("The file has invalid extension")
	{
	}
}
