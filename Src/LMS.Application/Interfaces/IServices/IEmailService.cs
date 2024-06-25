using static LMS.Application.Constants.ConstEnum;

namespace LMS.Application;

public interface IEmailService
{
    public void SendEmail(EmailDto email);
    public string GetMailContent(EmailHtmlTemplate emailType, Dictionary<string, string> keyValues);

}
