namespace ConfigDemo.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
        public bool EnableSsl { get; set; }
    }

}
