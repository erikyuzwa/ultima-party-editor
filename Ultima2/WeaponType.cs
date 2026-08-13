using System.ComponentModel;

namespace UltimaSaveEditor.Ultima2;

public enum WeaponType
{
    Dagger = 0,
    Mace,
    Axe,
    Bow,
    Sword,

    [Description("Great Sword")]
    GreatSword,

    [Description("Light Sword")]
    LightSword,

    Phaser,
    Quicksword
}