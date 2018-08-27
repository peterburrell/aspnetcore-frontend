namespace Demo.Web.Features.Error
{
    public class ErrorInfo
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}