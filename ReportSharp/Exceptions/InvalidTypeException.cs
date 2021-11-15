using System;

namespace ReportSharp.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(Type type, string typeName)
            : base($"The type {(object) type} is not a valid {typeName} type.")
        {
        }
    }
}