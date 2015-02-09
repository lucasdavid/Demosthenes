using System;
using System.Runtime.Serialization;

namespace Demosthenes.Services.Exceptions
{
    public class StudentNotEnrolledException : ApplicationException
    {
        // Summary:
        //     Initializes a new instance of the StudentNotEnrolledException class.
        public StudentNotEnrolledException() : base() { }
        //
        // Summary:
        //     Initializes a new instance of the StudentNotEnrolledException class with
        //     a specified error message.
        //
        // Parameters:
        //   message:
        //     A message that describes the error.
        public StudentNotEnrolledException(string message) : base(message) { }
        //
        // Summary:
        //     Initializes a new instance of the StudentNotEnrolledException class with
        //     serialized data.
        //
        // Parameters:
        //   info:
        //     The object that holds the serialized object data.
        //
        //   context:
        //     The contextual information about the source or destination.
        protected StudentNotEnrolledException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        //
        // Summary:
        //     Initializes a new instance of the StudentNotEnrolledException class with
        //     a specified error message and a reference to the inner exception that is
        //     the cause of this exception.
        //
        // Parameters:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   innerException:
        //     The exception that is the cause of the current exception. If the innerException
        //     parameter is not a null reference, the current exception is raised in a catch
        //     block that handles the inner exception.
        public StudentNotEnrolledException(string message, Exception innerException) : base (message, innerException) { }
    }
}
