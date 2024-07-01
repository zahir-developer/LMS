using System.Net;

namespace LMS.Application;

public class ResultDto
{
    public bool Result { get; set; }

    public HttpStatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public ResultDto()
    {
        this.StatusCode = HttpStatusCode.OK;
    }

}
