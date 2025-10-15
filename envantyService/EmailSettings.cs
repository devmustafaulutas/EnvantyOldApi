namespace envantyService
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
    public class EmailRequest
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    //public class EmailHumanRequest
    //{
    //    public string Subject { get; set; }
    //    public string Body { get; set; }
    //}

    //public class PasswordSendCode
    //{
    //    public string RecipientEmail { get; set; }
    //}
    //public class MailChangeSendCode
    //{
    //    public string RecipientEmail { get; set; }
    //}

    //Cip
    //public class EmailMobilRequest
    //{
    //    public string RecipientEmail { get; set; }
    //    public string Title { get; set; }
    //    public EmailMobil EmailMobil { get; set; }
    //}

    //public class EmailMobil
    //{
    //    public string NameSurname { get; set; }
    //    public string KayitTipi { get; set; }
    //    public int TakipNumarisi { get; set; }
    //    public string? DepartmanName { get; set; }
    //    public string? DepartmanCategoryName { get; set; }
    //    public string Title { get; set; }
    //    public string Content { get; set; }
    //}
    //CİP son
}
