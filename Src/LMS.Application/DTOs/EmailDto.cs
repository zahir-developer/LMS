using System.Security.AccessControl;
using static LMS.Application.Constants.ConstEnum;

namespace LMS.Application;

public class EmailDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string DisplayNameSender { get; set; }
    public Dictionary<string,string> EmailKeyValues { get; set; }
    public EmailHtmlTemplate EmailType { get; set; }
    
}
