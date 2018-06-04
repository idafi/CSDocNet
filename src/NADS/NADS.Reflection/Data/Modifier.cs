using System;

namespace NADS.Reflection.Data
{
    [Flags]
    public enum Modifier
    {
        None        = 0x0000,

        Abstract	= 0x0001,
        Async		= 0x0002,
        Const		= 0x0004,
        Extern		= 0x0008,
        Override	= 0x0010,
        Ref			= 0x0020,
        Readonly	= 0x0040,
        Sealed		= 0x0080,
        Static		= 0x0100,
        Virtual		= 0x0200,
        Volatile	= 0x0400
    }
}