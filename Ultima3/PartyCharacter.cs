using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using UltimaSaveEditor.Ultima4;

namespace UltimaSaveEditor.Ultima3;

public sealed class PartyCharacter
{
    public string Name { get; set; } =
        string.Empty;

    public HealthStatus Health { get; set; } =
        HealthStatus.Good;

    public byte Strength { get; set; }
    public byte Dexterity { get; set; }
    public byte Intelligence { get; set; }
    public byte Wisdom { get; set; }

    public RaceType Race { get; set; }
    public ClassType Class { get; set; }
    public SexType Sex { get; set; }

    public byte MagicPoints { get; set; }

    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Experience { get; set; }

    public ArmorType Armor { get; set; } =
        ArmorType.Skin;

    public WeaponType Weapon { get; set; } =
        WeaponType.Hands;

    //
    // Inventory
    //
    public ushort Food { get; set; }
    public ushort Gold { get; set; }

    public byte Torches { get; set; }
    public byte MagicGems { get; set; }
    public byte SkullKeys { get; set; }
    public byte TimeStopPowder { get; set; }

    //
    // Quantity arrays do not include
    // Hands or Skin.
    //
    public byte[] WeaponQuantities { get; } =
        new byte[15];

    public byte[] ArmorQuantities { get; } =
        new byte[7];

    //
    // Marks
    //
    public bool ForceMark { get; set; }
    public bool FireMark { get; set; }
    public bool SnakeMark { get; set; }
    public bool KingsMark { get; set; }

    //
    // Cards
    //
    public bool LoveCard { get; set; }
    public bool SolCard { get; set; }
    public bool MoonCard { get; set; }
    public bool DeathCard { get; set; }
}