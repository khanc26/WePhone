namespace WePhone.Models
{
    public class UserEmailOption
    {
        public List<string> ToMails { get; set; } // send to how many mails
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<KeyValuePair<string,string>> PlaceHolder { get; set; }

    }
}
