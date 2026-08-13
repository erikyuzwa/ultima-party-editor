using UltimaSaveEditor.Ultima3;

namespace UltimaSaveEditor.Ultima5;

public sealed class PartyCharacter
{
    public string Name { get; set; } =
        string.Empty;

    public SexType Sex { get; set; }

    public ClassType Class { get; set; }

    public HealthStatus Health { get; set; }

    public byte Strength { get; set; }

    public byte Dexterity { get; set; }

    public byte Intelligence { get; set; }

    public byte MagicPoints { get; set; }

    public ushort HitPoints { get; set; }

    public ushort MaxHitPoints { get; set; }

    public ushort Experience { get; set; }

    public byte Level { get; set; }

    //
    // The save file actually stores six generic
    // equipment item IDs.
    //
    public EquipmentItemType Weapon { get; set; }
        = EquipmentItemType.None;

    public EquipmentItemType Armor { get; set; }
        = EquipmentItemType.None;

    public EquipmentItemType Helm { get; set; }
        = EquipmentItemType.None;

    public EquipmentItemType Shield { get; set; }
        = EquipmentItemType.None;

    public EquipmentItemType Amulet { get; set; }
        = EquipmentItemType.None;

    public EquipmentItemType Ring { get; set; }
        = EquipmentItemType.None;
}