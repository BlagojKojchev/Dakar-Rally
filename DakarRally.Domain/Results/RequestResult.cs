namespace DakarRally.Domain.Results
{
    public class RequestResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }

    public class RequestResult<TResult> : RequestResult
    {
        public TResult Payload { get; set; }
    }
}
