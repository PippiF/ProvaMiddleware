using Serilog;

namespace TestProvaMiddleware.Exceptions
{
    public class ProvaTestException : Exception
    {
        //public required string MessageCustom { get; set; } = "Unknow Message";

        public ProvaTestException(string? message="") 
            : base(message)
        {
            CreateLog(Message + message);
        }

        public void CreateLog(string Message)
        {
            Log.Error(Message);
        }
    }
}
