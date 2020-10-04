using System;

namespace DesafioLin.API.Middleware
{
    public class ExceptionResponse
    {
        public string Source { get; private set; }
        public string Message { get; private set; }
        public string InnerException { get; private set; }
        public string StackTrace { get; private set; }

        public ExceptionResponse(Exception exception)
        {
            Source = exception.Source;
            Message = exception.Message;
            InnerException = exception.InnerException?.Message;
            StackTrace = exception.StackTrace;
        }
    }
}