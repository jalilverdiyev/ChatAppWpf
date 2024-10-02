using System.Windows.Controls;

namespace ChatApp.Wpf.Views;

public partial class ChatView : UserControl
{
	public ChatView()
	{
		InitializeComponent();
		MessagesScroll.ScrollToEnd();
	}
}
