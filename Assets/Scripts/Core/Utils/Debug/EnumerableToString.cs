namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Text;

    public static class EnumerableToString
    {
        public static string ToReadableString<T>(this IEnumerable<T> enumerable, int maxElems)
        {
            Contract.Requires(enumerable != null);

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int i = 0;
            foreach (T obj in enumerable)
            {
                if (i != 0)
                {
                    sb.Append(", ");
                }

                if (i++ == maxElems)
                {
                    sb.Append("...");
                    break;
                }

                sb.Append(obj.ToString());
            }

            sb.Append("]");
            return sb.ToString();
        }
    }
}
