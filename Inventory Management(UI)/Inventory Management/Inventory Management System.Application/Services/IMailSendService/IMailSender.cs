namespace Inventory_Management_System.Application.Services.IMailSendService
{
    public interface IMailSender
    {
        Task SendMailToUser(IEnumerable<string?>emailList);
    }
}
