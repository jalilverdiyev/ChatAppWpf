using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ChatApp.Wpf.Controls;

public partial class MessageControl : UserControl
{
	public MessageControl()
	{
		InitializeComponent();
		DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(900));
		BeginAnimation(OpacityProperty, fadeIn);
	}
}
