namespace ChatApp.Application.Helpers;

public static class FileHelper
{
	/// <summary>
	/// A method that checks if file has correct extension or not
	/// </summary>
	/// <param name="filename">A path of file</param>
	/// <param name="acceptedExtensions">Accepted extension list</param>
	/// <returns></returns>
	public static bool IsCorectExtension(string filename, params string[] acceptedExtensions)
	{
		var ext = Path.GetExtension(filename);
		return acceptedExtensions.Any(ae => ae == ext);
	}
}
