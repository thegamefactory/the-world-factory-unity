using System;
using System.Diagnostics;

namespace TWF
{
    public static class TwfDebug
    {
        public class AssertionException : Exception
        {
            private AssertionException(string message) : base(message) { }

            public static void Throw(StackTrace stackTrace, string message)
            {
                var frame = stackTrace.GetFrame(1);
                var fileName = frame.GetFileName();
                var line = frame.GetFileLineNumber();
                throw new AssertionException(message + " (" + fileName + ":" + line.ToString() + ")");
            }
        }

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

    }
}
