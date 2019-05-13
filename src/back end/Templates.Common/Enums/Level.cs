using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Templates.Common.Enums
{
    [Flags]
    public enum Level : byte
    {
        [DisplayName("一级")]
        [Description("最高水平")]
        One = 1,

        [DisplayName("二级")]
        [Description("较高水平")]
        Two = 2,

        [Description("普通水平")]
        Three = 4,

        [Description("较差水平")]
        Four = 8,

        Five = 16,

        Six = 32,

        [EnumMember(Value = "7")]
        Seven = 64,

        Eight = 128,

    }
}
