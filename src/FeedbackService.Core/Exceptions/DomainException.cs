using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace FeedbackService.Core.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException() : base() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception inner) : base(message, inner)
        { 
        }

        public DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
