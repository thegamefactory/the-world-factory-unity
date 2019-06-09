namespace TWF
{
    using System.Diagnostics;

    /// <summary>
    /// A minimal assertion package which is only compiled when the DEBUG symbol is defined.
    /// This should not be necessary but I could not get the System.Diagnostics assertions to work as desired.
    /// </summary>
    public static partial class TwfDebug
    {
        public static void Assert(bool condition)
        {
#if DEBUG
            if (!condition)
            {
                AssertionException.Throw(new StackTrace(true), string.Empty);
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
