using Microsoft.Extensions.Configuration;

namespace BoilerplateWebApi.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            this._mailTo = configuration["mailSettings:mailToAddress"];
            this._mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            //Send mail - output to console window

            Console.WriteLine($"Mail From {_mailFrom} to {_mailTo}, " + $"with  {nameof(LocalMailService)}");
            Console.WriteLine($" Subject: {subject}");
            Console.WriteLine($" Message: {message}");

        }
    }
}
