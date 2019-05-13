using System;
using System.Collections.Generic;
using System.Text;

namespace Templates.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class NotNullAttribute : Attribute
    {
    }
}
