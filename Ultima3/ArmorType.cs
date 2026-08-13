using System.ComponentModel;

namespace UltimaSaveEditor.Ultima3;

public enum ArmorType : byte
{
    Skin = 0,
    Cloth = 1,
    Leather = 2,
    Chain = 3,
    Plate = 4,

    [Description("+2 Chain")]
    Plus2Chain = 5,

    [Description("+2 Plate")]
    Plus2Plate = 6,

    [Description("Exotic Armor")]
    ExoticArmor = 7
}