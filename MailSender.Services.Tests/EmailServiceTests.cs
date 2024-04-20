using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.Services.Tests
{
    [TestClass]
    public class EmailServiceTests
    {
        private const string USERNAME = "levelkuldo@vasvari.org";
        private const string PASSWORD = "Vasv4riLev3l";
        private const string HOST = "smtp.gmail.com";
        private const int PORT = 587;

        [TestMethod()]
        public async Task SendMailAsyncTest()
        {

            var emailService = new EmailService(USERNAME, PASSWORD, HOST, PORT);
            try
            {
                string from = "elek@teszt.hu";
                string to = "boros.bence@vasvari.org";
                string subject = "Teszt";
                string body = "<h1>Hello Világ</h1>";
                string fromName = "Teszt Elek";
                await emailService.SendMailAsync(from, to, subject, body, fromName);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                return;
            }
            Assert.IsTrue(true);
        }
    }
}