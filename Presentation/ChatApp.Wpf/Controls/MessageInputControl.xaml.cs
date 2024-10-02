using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChatApp.Wpf.ViewModels;
using Emoji.Wpf;

namespace ChatApp.Wpf.Controls;

public partial class MessageInputControl : UserControl
{
	public MessageInputControl()
	{
		InitializeComponent();
	}

	private void Input_OnGotFocus(object sender, RoutedEventArgs e)
	{
		if (MsgInput.Text == "Type your Message")
			MsgInput.Text = "";
	}

	private void MsgInput_OnLostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(MsgInput.Text))
			MsgInput.Text = "Type your Message";
	}

	private void MsgInput_OnKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key != Key.Enter || DataContext is not MessageInputViewModel model) return;

		model.Text = MsgInput.Text;
		model.SendMessageCommand.Execute(null);
	}

	private void Picker_OnPicked(object sender, EmojiPickedEventArgs e)
	{
		if (MsgInput.Text == "Type your Message")
			MsgInput.Text = e.Emoji;
		else
			MsgInput.Text += e.Emoji;
	}
}
