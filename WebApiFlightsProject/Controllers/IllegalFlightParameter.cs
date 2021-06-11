using System;
using System.Runtime.Serialization;

namespace WebAPI.Controllers
{
    [Serializable]
    internal class IllegalFlightParameter : Exception
    {
        public IllegalFlightParameter()
        {
        }

        public IllegalFlightParameter(string message) : base(message)
        {
        }

        public IllegalFlightParameter(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IllegalFlightParameter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}