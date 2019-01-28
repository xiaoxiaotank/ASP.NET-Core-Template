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
        One = 0x1,

        [DisplayName("二级")]
        [Description("较高水平")]
        Two = 0x2,

        [Description("普通水平")]
        Three = 0x4,

        [Description("较差水平")]
        Four = 0x8,

        Five = 0x10,

        Six = 0x20,

        [EnumMember(Value = "7")]
        Seven = 0x40,

        Eight = 0x80,

    }
}
