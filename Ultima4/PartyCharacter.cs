namespace UltimaSaveEditor.Ultima4;

public sealed class PartyCharacter
{
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Experience { get; set; }

    public ushort Strength { get; set; }
    public ushort Dexterity { get; set; }
    public ushort Intelligence { get; set; }

    public ushort MagicPoints { get; set; }

    public ushort Unknown { get; set; }

    public WeaponType Weapon { get; set; }
        = WeaponType.Hands;

    public ArmorType Armor { get; set; }
        = ArmorType.Skin;

    public string Name { get; set; }
        = string.Empty;

    public byte Sex { get; set; }

    public byte ClassType { get; set; }

    public byte Status { get; set; }
}