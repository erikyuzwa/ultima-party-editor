namespace UltimaSaveEditor.Ultima1;

public sealed class PartyCharacter
{
    public string Name { get; set; } =
        string.Empty;

    public SexType Sex { get; set; }

    public RaceType Race { get; set; }

    public ClassType Class { get; set; }

    public ushort HitPoints { get; set; }

    public ushort Experience { get; set; }

    public ushort Strength { get; set; }

    public ushort Agility { get; set; }

    public ushort Stamina { get; set; }

    public ushort Charisma { get; set; }

    public ushort Wisdom { get; set; }

    public ushort Intelligence { get; set; }
}