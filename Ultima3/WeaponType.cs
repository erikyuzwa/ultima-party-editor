using System.ComponentModel;

namespace UltimaSaveEditor.Ultima3;

public enum WeaponType : byte
{
    Hands = 0,
    Dagger = 1,
    Mace = 2,
    Sling = 3,
    Axe = 4,
    Bow = 5,
    Sword = 6,

    [Description("2H Sword")]
    TwoHandedSword = 7,

    [Description("+2 Axe")]
    Plus2Axe = 8,

    [Description("+2 Bow")]
    Plus2Bow = 9,

    [Description("+2 Sword")]
    Plus2Sword = 10,

    Gloves = 11,

    [Description("+4 Axe")]
    Plus4Axe = 12,

    [Description("+4 Bow")]
    Plus4Bow = 13,

    [Description("+4 Sword")]
    Plus4Sword = 14,

    [Description("Exotic Weapon")]
    ExoticWeapon = 15
}