using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MailSender.Services;

namespace MailSender.MAUI.ViewModels
{
    public /*partial*/ class LogViewModel : ObservableObject
    {
        private readonly LogService _logService;
		private string? _logText;
		public string? LogText
		{
			get { return _logText; }
			set { 
                SetProperty(ref _logText, value);
            }
		}

        // MVVM Toolkit kódgenerátor, partial osztály kell hozzá
        // https://learn.microsoft.com/hu-hu/dotnet/communitytoolkit/mvvm/generators/overview
        //[ObservableProperty]
        //private string? _logText2;

        public LogViewModel(LogService logService)
        {
            _logService = logService;
            UpdateView();
            // Feliratkozik egy string üzenetküldő csatornára
            WeakReferenceMessenger.Default.Register<string>(this, (r, m)
                => UpdateView()
            );
        }

        private void UpdateView()
        {
            LogText = string.Join(Environment.NewLine, _logService.Logs);
        }
    }
}
