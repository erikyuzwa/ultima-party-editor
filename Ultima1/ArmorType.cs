using System.ComponentModel;

namespace UltimaSaveEditor.Ultima1;

public enum ArmorType
{
    Leather = 0,

    [Description("Chain Mail")]
    ChainMail,

    [Description("Plate Mail")]
    PlateMail,

    [Description("Vacuum Suit")]
    VacuumSuit,

    [Description("Reflect Suit")]
    ReflectSuit
}