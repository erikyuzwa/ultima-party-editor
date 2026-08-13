using System.ComponentModel;

namespace UltimaSaveEditor.Ultima4;

public enum SpellMixtureType
{
    Awaken = 0,
    Blink,
    Cure,
    Dispel,
    Energy,
    Fireball,
    Gate,
    Heal,
    Iceball,
    Jinx,
    Kill,
    Light,
    MagicMissile,
    Negate,
    Open,
    Protection,
    Quickness,
    Resurrect,
    Sleep,
    Tremor,
    Undead,
    View,
    Winds,

    [Description("X-It")]
    XIt,

    [Description("Y-Up")]
    YUp,

    [Description("Z-Down")]
    ZDown
}