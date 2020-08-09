using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LargeTextEditor
{

    public delegate Task AsyncEventHandler(
        object sender, EventArgs e);

    public delegate Task AsyncEventHandler<TEventArgs>(
        object sender, TEventArgs e)
        where TEventArgs : EventArgs;


    public static class Extensions
    {

        public static IEnumerable<(T value, int index)> Indexed<T>(this IEnumerable<T> list)
        {
            return list.Select((value, index) => (value, index));
        }

        public static string Join(this IEnumerable<string> list, string separator)
        {
            return string.Join(separator, list.ToArray());
        }

        public static string Join(this IEnumerable<object> list, string separator)
        {
            return string.Join(separator, list.ToArray());
        }

        public static string GetString(this Encoding encoding, string text, int byteLength)
        {
            return GetString(encoding, text, 0, byteLength);
        }

        public static string GetString(this Encoding encoding, string text, int byteIndex, int byteLength)
        {
            var bytes = encoding.GetBytes(text);

            byteLength = byteLength.Limit(0, bytes.Length - byteIndex);

            if (byteLength <= 0)
            {
                return "";
            }

            var bytesDest = new byte[byteLength];

            Array.Copy(bytes, byteIndex, bytesDest, 0, byteLength);

            var textDest = encoding.GetString(bytesDest);

            return textDest;
        }


        public static int Max(this int value, int maxValue)
        {
            return (value <= maxValue) ? value : maxValue;
        }

        public static int Min(this int value, int minValue)
        {
            return (value >= minValue) ? value : minValue;
        }

        public static int Limit(this int value, int minValue, int maxValue)
        {
            return value.Max(maxValue).Min(minValue);
        }


        public static long Max(this long value, long maxValue)
        {
            return (value <= maxValue) ? value : maxValue;
        }

        public static long Min(this long value, long minValue)
        {
            return (value >= minValue) ? value : minValue;
        }

        public static long Limit(this long value, long minValue, long maxValue)
        {
            return value.Min(minValue).Max(maxValue);
        }

        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, new object[] { });
            }
            else
            {
                action();
            }
        }

        public static T InvokeIfRequired<T>(this Control control, Func<T> action)
        {
            if (control.InvokeRequired)
            {
                return (T)control.Invoke(action, new object[] { });
            }
            else
            {
                return action();
            }
        }

        public static IEnumerable<ListViewItem> AsEnumerable(
            this ListView.ListViewItemCollection ListViewItems)
        {
            for (int index = 0; index < ListViewItems.Count; index++)
            {
                var ListViewItem = ListViewItems[index];

                yield return ListViewItem;
            }
        }

        public static IEnumerable<ListViewItem> AsEnumerable(
            this ListView.SelectedListViewItemCollection ListViewItems)
        {
            for (int index = 0; index < ListViewItems.Count; index++)
            {
                var ListViewItem = ListViewItems[index];

                yield return ListViewItem;
            }
        }

        public static string Escape(this string text)
        {
            var buf = new StringBuilder();

            foreach (var c in text)
            {
                if (c == '\\')
                {
                    buf.Append(@"\\");
                }
                else
                {
                    buf.Append(c);
                }
            }

            return buf.ToString();
        }

        public static T[] Take<T>(this T[] source, long count)
        {
            return source.Take(0, count);
        }

        public static T[] Take<T>(this T[] source, long index, long count)
        {
            if (index < 0)
            {
                index = (index + source.Length).Min(0);
            }

            if (source.LongLength - index < count)
            {
                count = source.LongLength - index;
            }

            if (count == 0)
            {
                return new T[0];
            }

            T[] buf = new T[count];

            Array.Copy(source, index, buf, 0, count);

            return buf;
        }

        public static T[] Concat<T>(this T[] source, T[] append)
        {
            var dest = new T[source.Length + append.Length];

            Array.Copy(source, dest, source.Length);
            Array.Copy(append, 0, dest, source.Length, append.Length);

            return dest;
        }

        public static T[] DefaultIfEmpty<T>(this T[] source)
        {
            return source.DefaultIfEmpty(default(T));
        }

        public static T[] DefaultIfEmpty<T>(this T[] source, T defaultValue)
        {
            if (source == null
                || source.Length == 0)
            {
                return new T[] { defaultValue };
            }

            return source;
        }

    }

}
