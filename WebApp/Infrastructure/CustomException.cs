using System.Runtime.Serialization;

namespace WebApp.Infrastructure
{
    public class CustomException<T> : Exception where T : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception innerException) : base(message, innerException) { }
        public CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public class MyCustomException : Exception { }
        public class SomeOtherException : Exception { }
    }
}
