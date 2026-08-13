using UltimaSaveEditor.Ultima3;

namespace UltimaSaveEditor.Ultima2;

public sealed class PartyCharacter
{
    public string Name { get; set; } =
        string.Empty;

    public SexType Sex { get; set; }

    public RaceType Race { get; set; }

    public ClassType Class { get; set; }

    public ushort HitPoints { get; set; }

    public ushort Experience { get; set; }

    public byte Strength { get; set; }

    public byte Stamina { get; set; }

    public byte Wisdom { get; set; }

    public byte Agility { get; set; }

    public byte Charisma { get; set; }

    public byte Intelligence { get; set; }
}