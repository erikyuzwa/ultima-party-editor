using System.ComponentModel;

namespace UltimaSaveEditor.Ultima5;

public enum WeaponType
{
    Dagger = 0,
    Sling,
    Club,
    FlamingOil,
    MainGauche,
    Spear,
    ThrowingAxe,
    ShortSword,
    Mace,
    MorningStar,
    Bow,
    Arrows,
    Crossbow,
    Quarrels,
    LongSword,

    [Description("2H Hammer")]
    TwoHandedHammer,

    [Description("2H Axe")]
    TwoHandedAxe,

    [Description("2H Sword")]
    TwoHandedSword,

    Halberd,
    ChaosSword,
    MagicBow,
    SilverSword,
    MagicAxe,
    GlassSword,
    JeweledSword,
    MysticSword
}