using MailSender.MAUI.ViewModels;

namespace MailSender.MAUI.Views;

public partial class LogPage : ContentPage
{
	public LogPage(LogViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}