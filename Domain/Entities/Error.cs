namespace WebAPI.Domain.Entities
{
    public class Error(string errorData, DateTime errorDate, int errorCode, ErrorCategory category)
    {
        public string ErrorData { get; set; } = errorData;
        public DateTime ErrorDate { get; set; } = errorDate;

        public int ErrorCode { get; set; } = errorCode;

        public ErrorCategory Category { get; set; } = category;
    }
    public enum ErrorCategory
    {
        SensorError,
        BrokerError,
        AuthenticationError,
        SubscriptionError,
        TimeoutError,
        UnknownError
    }

    

}