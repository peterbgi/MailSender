using MailSender.MAUI.ViewModels;

namespace MailSender.MAUI.Views;

public partial class MailPage : ContentPage
{
	public MailPage(MailViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

    private void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
		if (IsLoaded)
		{
			webView.Reload();
		}
    }
}