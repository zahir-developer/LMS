namespace LMS.Application.Config;


public class EmailConfig
{
    public string EmailId { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string FromEmail { get; set; }
    public string HtmlTemplatePath { get; set; }
}