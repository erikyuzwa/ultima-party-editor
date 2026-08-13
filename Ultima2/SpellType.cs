using System.ComponentModel;

namespace UltimaSaveEditor.Ultima2;

public enum SpellType
{
    Light = 0,

    [Description("Ladder Down")]
    LadderDown,

    [Description("Ladder Up")]
    LadderUp,

    Passwall,
    Surface,
    Prayer,

    [Description("Magic Missile")]
    MagicMissile,

    Blink,
    Kill
}