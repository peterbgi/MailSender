using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MailSender.Services;

namespace MailSender.MAUI.ViewModels
{
    public class MailViewModel : ObservableObject
    {
        private readonly EmailService _emailService;
        private readonly LogService _logService;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string? Subject { get; set; }

        private string? _body;
        public string? Body
        {
            get { return _body; }
            set 
            { 
                _body = value;
                // Ha a Body változik, akkor értesíti a HtmlBody tulajdonságot
                // hogy változott az értéke és frissítse a View-t
                OnPropertyChanged(nameof(HtmlBody));
            }
        }

        public string? HtmlBody => Body?.ReplaceLineEndings("<br />");
        //public string HtmlBody2 
        //{
        //    get 
        //    {
        //        return Body; 
        //    } 
        //}
        public string FromToolTip => "Az e-mail cím után szóközzel tudja megadni a megjelenítendő nevét.";
        public string ToToolTip => FromToolTip + " Több címzett esetén pontos vesszővel válassza el az értékeket.";
        public string HtmlBodyToolTip => "Az üzenetet HTML formátumban is el tudja küldeni.";

        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set { SetProperty(ref _visible, value); }
        }

        private bool _isSending;
        public bool IsSending
        {
            get { return _isSending; }
            set { SetProperty(ref _isSending, value); }
        }

        public IAsyncRelayCommand SendMailCommandAsync { get; }

        public MailViewModel(EmailService emailService, LogService logService)
        {
            _emailService = emailService;
            _logService = logService;
            SendMailCommandAsync = new AsyncRelayCommand(SendMail);
        }

        private async Task SendMail()
        {
            IsSending = true;
            Visible = false;
            await Task.Delay(1000);
            // Levélküldés logikája
            // Ha ki van töltve a feladó és a címzett mező
            if (!string.IsNullOrWhiteSpace(From) && !string.IsNullOrWhiteSpace(To))
            {
                string[] fromArray = GetDisplayName(From);
                string[] addresses = To.Split(';');
                foreach (var address in addresses)
                {
                    string[] toArray = GetDisplayName(address);
                    try
                    {
                        // levélküldés művelet
                        await _emailService.SendMailAsync(fromArray[0], toArray[0],
                            Subject, Body, fromArray[1], toArray[1]);
                        // Ha sikeres a levélküldés
                        _logService.Append($"Sikeres üzenet küldés: {toArray[0]}");
                    }
                    catch (Exception ex)
                    {
                        // Ha sikertelen a levélküldés
                        _logService.Append(ex.Message);
                    }
                }
            }
            else
            {
                _logService.Append("Hiányzik a feladó vagy a címzett e-mail címe.");
            }
            
            // Ő küldi egy üzenetet, a string típusú csatorára
            WeakReferenceMessenger.Default.Send("update");
            IsSending = false;
            Visible = true;
        }

        private string[] GetDisplayName(string inputText)
        {
            // email input felépítése: emailcím szóköz Megjelenítendő név
            // elek@teszt.hu Teszt Elekné Enikő
            string[] result = new string[2];
            result[0] = inputText.Split(' ')[0];
            // Átugorja az inputText első szóközzel elválasztott részét
            result[1] = inputText.Length > 0 ? string.Join(" ", inputText.Split(' ').Skip(1)) : string.Empty;
            return result;
        }
    }
}
