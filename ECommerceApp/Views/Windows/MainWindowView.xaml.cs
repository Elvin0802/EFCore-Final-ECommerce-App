using System.Windows;
using System.Windows.Navigation;

namespace ECommerceApp.Views.Windows;

public partial class MainWindowView : NavigationWindow
{
	public MainWindowView()
	{
		InitializeComponent();
		this.Closing += MainWindow_Closing;
	}
	private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
	{
		App.Current.Shutdown();
	}
}
