using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace FeedbackService.Core.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
