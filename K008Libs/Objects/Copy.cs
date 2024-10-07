using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K008Libs.Objects
{
    public static class Copy
    {
        public static T ShallowCopy<T>(this T source) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return (T)source.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(source, null);
        }
    }
}
