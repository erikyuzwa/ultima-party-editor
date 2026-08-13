using System.ComponentModel;

namespace UltimaSaveEditor.Ultima5;

public enum EquipmentItemType : byte
{
    LeatherHelm = 0x00,
    ChainCoif = 0x01,
    IronHelm = 0x02,
    SpikedHelm = 0x03,

    SmallShield = 0x04,
    LargeShield = 0x05,
    SpikedShield = 0x06,
    MagicShield = 0x07,
    JeweledShield = 0x08,

    Cloth = 0x09,
    Leather = 0x0A,
    RingMail = 0x0B,
    Scale = 0x0C,
    Chain = 0x0D,
    Plate = 0x0E,
    MysticArmor = 0x0F,

    Dagger = 0x10,
    Sling = 0x11,
    Club = 0x12,
    FlamingOil = 0x13,
    MainGauche = 0x14,
    Spear = 0x15,
    ThrowingAxe = 0x16,
    ShortSword = 0x17,
    Mace = 0x18,
    MorningStar = 0x19,
    Bow = 0x1A,
    Arrows = 0x1B,
    Crossbow = 0x1C,
    Quarrels = 0x1D,
    LongSword = 0x1E,

    [Description("2H Hammer")]
    TwoHandedHammer = 0x1F,

    [Description("2H Axe")]
    TwoHandedAxe = 0x20,

    [Description("2H Sword")]
    TwoHandedSword = 0x21,

    Halberd = 0x22,
    ChaosSword = 0x23,
    MagicBow = 0x24,
    SilverSword = 0x25,
    MagicAxe = 0x26,
    GlassSword = 0x27,
    JeweledSword = 0x28,
    MysticSword = 0x29,

    InvisibilityRing = 0x2A,
    ProtectionRing = 0x2B,
    RegenerationRing = 0x2C,

    AmuletOfTurning = 0x2D,
    SpikedCollar = 0x2E,
    Ankh = 0x2F,

    None = 0xFF
}