using System.ComponentModel;

namespace UltimaSaveEditor.Ultima1;

public enum SpellType
{
    Open = 0,
    Unlock,

    [Description("Magic Missile")]
    MagicMissile,

    Steal,

    [Description("Ladder Down")]
    LadderDown,

    [Description("Ladder Up")]
    LadderUp,

    Blink,
    Create,
    Destroy,
    Kill
}