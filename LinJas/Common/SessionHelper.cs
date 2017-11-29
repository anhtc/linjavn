using System.Web;

namespace LinJas.Common
{
    public static class SessionHelper<T>
    {
        public static void SetValue(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static T GetValue(string key)
        {
            var value = (T)HttpContext.Current.Session[key];
            return value;
        }

        public static bool HasKey(string key)
        {
            var hasKey = HttpContext.Current.Session[key] is T;
            return hasKey;
        }

        public static void Remove(string key)
        {
            if (HasKey(key))
            {
                HttpContext.Current.Session.Remove(key);
            }
        }
    }
}