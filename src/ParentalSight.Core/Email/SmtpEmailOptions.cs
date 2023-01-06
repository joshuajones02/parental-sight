namespace ParentalSight.Core.Email
{
    using ParentalSight;
    using System;
    using System.Linq;

    internal class SmtpEmailOptions : ISmtpEmailOptions
    {
        public SmtpEmailOptions(string host, int port, string sender, string username, string password, string[] recipients)
        {
            if (host is null)
                throw new ArgumentNullException(nameof(Host));
            if (sender is null)
                throw new ArgumentNullException(nameof(Sender));
            if (password is null)
                throw new ArgumentNullException(nameof(Password));
            if (username is null)
                throw new ArgumentNullException(nameof(Username));
            if (port == default)
                throw new ArgumentException(nameof(Port));
            if (recipients is null)
                throw new ArgumentNullException(nameof(Recipients));
            if (recipients.Any() == false)
                throw new ArgumentException(nameof(Recipients));

            Host = host;
            Port = port;
            Sender = sender;
            Username = username;
            Password = password;
            Recipients = recipients;
        }

        public int Port { get; protected set; }

        public string Host { get; protected set; }

        public string Sender { get; protected set; }

        public string Password { get; protected set; }

        public string Username { get; protected set; }

        public string[] Recipients { get; protected set; }
    }
}