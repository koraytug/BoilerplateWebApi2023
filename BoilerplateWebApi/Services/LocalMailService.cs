﻿namespace BoilerplateWebApi.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "admin@somecompany.com";
        private string _mailFrom = "noreply@somecompany.com";

        public void Send(string subject, string message)
        {
            //Send mail - output to console window

            Console.WriteLine($"Mail From {_mailFrom} to {_mailTo}, " + $"with  {nameof(LocalMailService)}");
            Console.WriteLine($" Subject: {subject}");
            Console.WriteLine($" Message: {message}");

        }
    }
}