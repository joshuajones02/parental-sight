namespace ParentalSight.Email
{
    using System.Threading.Tasks;

    public interface IEmailClient
    {
        Task SendAsync(string recipient, string subject, string body);
    }
}