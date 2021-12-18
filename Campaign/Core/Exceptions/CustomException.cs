using System;

namespace Campaign.Core.Exceptions
{
    [Serializable]
    class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public int? ErrorCode { get; set; }
        public CustomException() : base() { }
        public CustomException(int StatusCode, string message, int? ErrorCode = null) : base(message)
        {
            this.ErrorCode = ErrorCode;
            this.StatusCode = StatusCode;
        }
        public object ExceptionDetails()
        {
            return new
            {
                errorCode = ErrorCode,
                errorMessage = base.Message,
            };
        }

    }
}
