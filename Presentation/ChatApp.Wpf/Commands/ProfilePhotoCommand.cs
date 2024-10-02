using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using ChatApp.Application.Exceptions;
using ChatApp.Application.Helpers;
using ChatApp.Wpf.ViewModels;

namespace ChatApp.Wpf.Commands;

public class ProfilePhotoCommand(RegisterViewModel registerViewModel) : ICommand
{
	public RegisterViewModel RegisterViewModel { get; set; } = registerViewModel;

	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public void Execute(object? parameter)
	{
		var dialog = new OpenFileDialog();
		dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.JPEG)|*.BMP;*.JPG;*.PNG;*.JPEG";
		dialog.ShowDialog();

		if (!FileHelper.IsCorectExtension(dialog.FileName, ".bmp", ".png", ".jpg", ".jpeg"))
			throw new InvalidExtensionException();

		var dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		var uploadDir = Path.Combine(dir, "ChatApp");

		if (!Directory.Exists(uploadDir))
			Directory.CreateDirectory(uploadDir);

		var ext = Path.GetExtension(dialog.FileName);
		var uploadName = $"{Guid.NewGuid():N}{ext}";
		var stream = File.OpenWrite(Path.Combine(uploadDir, uploadName));
		var selectedStream = dialog.OpenFile();

		selectedStream.CopyTo(stream);
		stream.Close();
		selectedStream.Close();

		registerViewModel.ProfilePhoto = Path.Combine(uploadDir, uploadName);
	}

	public event EventHandler? CanExecuteChanged;
}
