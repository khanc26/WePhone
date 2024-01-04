namespace WePhone.Services
{
    public interface IEmailService
    {
        //Task SendEmailForForgotPassword(UserEmailOption userEmailOptions);
        Task TestMail(UserEmailOption userEmailOption);
    }
}