namespace DNT2017
{
    public class EmailMessageSettings
    {
        public string SmptpServer { get; set; }

        public int Port { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string MailRecipient { get; set; }
    }
}