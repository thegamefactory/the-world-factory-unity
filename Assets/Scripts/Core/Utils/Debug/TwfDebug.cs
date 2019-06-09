namespace TWF
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// A minimal assertion package which is only compiled when the DEBUG symbol is defined.
    /// This should not be necessary but I could not get the System.Diagnostics assertions to work as desired.
    /// </summary>
    public static class TwfDebug
    {
        public static void Assert(bool condition)
        {
#if DEBUG
            if (!condition)
            {
                AssertionException.Throw(new StackTrace(true), "");
            }
#endif
        }

        public static void Assert(bool condition, string message)
        {
#if DEBUG
            if (!condition)
            {
                AssertionException.Throw(new StackTrace(true), message);
            }
#endif
        }

        public class AssertionException : Exception
        {
            private AssertionException(string message)
                : base(message) { }

            public static void Throw(StackTrace stackTrace, string message)
            {
                var frame = stackTrace.GetFrame(1);
                var fileName = frame.GetFileName();
                var line = frame.GetFileLineNumber();
                throw new AssertionException(message + " (" + fileName + ":" + line.ToString() + ")");
            }
        }
    }
}
