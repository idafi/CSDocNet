using System;

namespace NADS.Reflection
{
    [Flags]
    public enum Modifier
    {
        Abstract	= 0x0001,
        Async		= 0x0002,
        Const		= 0x0004,
        Extern		= 0x0008,
        Override	= 0x0010,
        Partial		= 0x0020,
        Ref			= 0x0040,
        Readonly	= 0x0080,
        Sealed		= 0x0100,
        Static		= 0x0200,
        Unsafe		= 0x0400,
        Virtual		= 0x0800,
        Volatile	= 0x1000
    }
}