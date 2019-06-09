namespace TWF
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class AssertionException : Exception
    {
        private AssertionException()
        {
        }

        private AssertionException(string message)
           : base(message)
        {
        }

        private AssertionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static void Throw(StackTrace stackTrace, string message)
        {
            Contract.Requires(stackTrace != null);

            var frame = stackTrace.GetFrame(1);
            var fileName = frame.GetFileName();
            var line = frame.GetFileLineNumber();
#pragma warning disable CA1305 // Specify IFormatProvider
            throw new AssertionException(message + " (" + fileName + ":" + Convert.ToString(line) + ")");
#pragma warning restore CA1305 // Specify IFormatProvider
        }
    }
}
