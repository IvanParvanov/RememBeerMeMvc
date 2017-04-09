using System.Net.Mail;
using System.Threading.Tasks;

using Bytes2you.Validation;

using MailJet.Client;

using Microsoft.AspNet.Identity;

namespace RememBeer.Common.Services
{
    public class MailjetEmailService : IIdentityMessageService
    {
        private readonly string senderEmail;
        private readonly string userName;
        private readonly string password;

        public MailjetEmailService(string userName, string password, string sender)
        {
            Guard.WhenArgument(sender, nameof(sender)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(userName, nameof(userName)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(password, nameof(password)).IsNullOrEmpty().Throw();

            this.senderEmail = sender;
            this.userName = userName;
            this.password = password;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Run(() =>
                           {
                               var client = new MailJetClient(this.userName, this.password);
                               var mailMessage = new MailMessage(this.senderEmail, message.Destination, message.Subject, message.Body);
                               client.SendMessage(mailMessage);
                           });
        }
    }
}
