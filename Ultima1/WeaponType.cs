using System.ComponentModel;

namespace UltimaSaveEditor.Ultima1;

public enum WeaponType
{
    Dagger = 0,
    Mace,
    Axe,

    [Description("Rope & Spikes")]
    RopeAndSpikes,

    Sword,

    [Description("Great Sword")]
    GreatSword,

    [Description("Bow & Arrows")]
    BowAndArrows,

    Amulet,
    Wand,
    Staff,
    Triangle,
    Pistol,

    [Description("Light Sword")]
    LightSword,

    Phazor,
    Blaster
}