using System;
using System.Runtime.Serialization;

namespace Demosthenes.Services.Exceptions
{
    public class ScheduleConflictBetweenClassesException : ApplicationException
    {
        // Summary:
        //     Initializes a new instance of the ScheduleConflictBetweenClassesException class.
        public ScheduleConflictBetweenClassesException() : base() { }
        //
        // Summary:
        //     Initializes a new instance of the ScheduleConflictBetweenClassesException class with
        //     a specified error message.
        //
        // Parameters:
        //   message:
        //     A message that describes the error.
        public ScheduleConflictBetweenClassesException(string message) : base(message) { }
        //
        // Summary:
        //     Initializes a new instance of the ScheduleConflictBetweenClassesException class with
        //     serialized data.
        //
        // Parameters:
        //   info:
        //     The object that holds the serialized object data.
        //
        //   context:
        //     The contextual information about the source or destination.
        protected ScheduleConflictBetweenClassesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        //
        // Summary:
        //     Initializes a new instance of the ScheduleConflictBetweenClassesException class with
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
        public ScheduleConflictBetweenClassesException(string message, Exception innerException) : base (message, innerException) { }
    }
}
