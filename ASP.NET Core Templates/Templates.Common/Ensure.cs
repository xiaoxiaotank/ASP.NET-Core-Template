using System;
using System.Collections.Generic;
using System.Text;

namespace Templates.Common
{
    public static class Ensure
    {
        public static void NotNull<T>(T param, string paramName) where T : class
        {
            if(param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
