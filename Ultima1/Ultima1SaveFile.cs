using System.Buffers.Binary;
using System.Text;
using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima1;

public sealed class Ultima1SaveFile
    : ISaveFile
{
    public const int FileSize = 820;

    private const int NameOffset =
        0x00;

    private const int NameLength =
        15;

    private const int RaceOffset =
        0x10;

    private const int ClassOffset =
        0x12;

    private const int SexOffset =
        0x14;

    private const int HitPointsOffset =
        0x16;

    private const int StrengthOffset =
        0x18;

    private const int AgilityOffset =
        0x1A;

    private const int StaminaOffset =
        0x1C;

    private const int CharismaOffset =
        0x1E;

    private const int WisdomOffset =
        0x20;

    private const int IntelligenceOffset =
        0x22;

    private const int ExperienceOffset =
        0x26;

    private const int GoldOffset =
    0x24;

    private const int FoodOffset =
        0x28;

    private const int RedGemOffset =
        0x4C;

    private const int GreenGemOffset =
        0x4E;

    private const int BlueGemOffset =
        0x50;

    private const int WhiteGemOffset =
        0x52;

    private const int ArmorOffset =
        0x56;

    private const int WeaponOffset =
        0x62;

    private const int SpellOffset =
        0x82;

    private const int TransportOffset =
    0x30;

    private const int PlayerXOffset =
        0x34;

    private const int PlayerYOffset =
        0x36;

    private byte[] bytes =
        Array.Empty<byte>();

    private readonly PartyCharacter character =
        new();

    public string? Filename
    {
        get;
        private set;
    }

    public bool IsLoaded =>
        bytes.Length == FileSize;

    public PartyCharacter Character =>
        character;

    public TransportType Transport
    {
        get =>
            (TransportType)
                ReadUInt16(
                    TransportOffset);

        set =>
            WriteUInt16(
                TransportOffset,
                (ushort)value);
    }

    public ushort PlayerX
    {
        get =>
            ReadUInt16(
                PlayerXOffset);

        set =>
            WriteUInt16(
                PlayerXOffset,
                value);
    }

    public ushort PlayerY
    {
        get =>
            ReadUInt16(
                PlayerYOffset);

        set =>
            WriteUInt16(
                PlayerYOffset,
                value);
    }
    public ushort Gold
    {
        get =>
            ReadUInt16(
                GoldOffset);

        set =>
            WriteUInt16(
                GoldOffset,
                value);
    }

    public ushort Food
    {
        get =>
            ReadUInt16(
                FoodOffset);

        set =>
            WriteUInt16(
                FoodOffset,
                value);
    }

    public ushort RedGem
    {
        get =>
            ReadUInt16(
                RedGemOffset);

        set =>
            WriteUInt16(
                RedGemOffset,
                value);
    }

    public ushort GreenGem
    {
        get =>
            ReadUInt16(
                GreenGemOffset);

        set =>
            WriteUInt16(
                GreenGemOffset,
                value);
    }

    public ushort BlueGem
    {
        get =>
            ReadUInt16(
                BlueGemOffset);

        set =>
            WriteUInt16(
                BlueGemOffset,
                value);
    }

    public ushort WhiteGem
    {
        get =>
            ReadUInt16(
                WhiteGemOffset);

        set =>
            WriteUInt16(
                WhiteGemOffset,
                value);
    }

    public ushort GetArmorQuantity(
    ArmorType armor)
    {
        return ReadUInt16(
            ArmorOffset +
            ((int)armor * 2));
    }

    public void SetArmorQuantity(
        ArmorType armor,
        ushort quantity)
    {
        WriteUInt16(
            ArmorOffset +
            ((int)armor * 2),
            quantity);
    }

    public ushort GetWeaponQuantity(
        WeaponType weapon)
    {
        return ReadUInt16(
            WeaponOffset +
            ((int)weapon * 2));
    }

    public void SetWeaponQuantity(
        WeaponType weapon,
        ushort quantity)
    {
        WriteUInt16(
            WeaponOffset +
            ((int)weapon * 2),
            quantity);
    }

    public ushort GetSpellQuantity(
        SpellType spell)
    {
        return ReadUInt16(
            SpellOffset +
            ((int)spell * 2));
    }

    public void SetSpellQuantity(
        SpellType spell,
        ushort quantity)
    {
        WriteUInt16(
            SpellOffset +
            ((int)spell * 2),
            quantity);
    }

    public void Load(
        string filename)
    {
        byte[] data =
            File.ReadAllBytes(filename);

        if (data.Length != FileSize)
        {
            throw new InvalidDataException(
                $"Invalid Ultima I save file. " +
                $"Expected {FileSize} bytes, " +
                $"found {data.Length}.");
        }

        bytes = data;
        Filename = filename;

        ReadCharacter();
    }

    public void Save()
    {
        if (!IsLoaded)
        {
            throw new InvalidOperationException(
                "No PLAYER1.U1 file is loaded.");
        }

        if (string.IsNullOrWhiteSpace(
                Filename))
        {
            throw new InvalidOperationException(
                "No filename is available.");
        }

        WriteCharacter();

        CreateBackup(
            Filename);

        File.WriteAllBytes(
            Filename,
            bytes);
    }

    public void SaveAs(
        string filename)
    {
        if (!IsLoaded)
        {
            throw new InvalidOperationException(
                "No PLAYER1.U1 file is loaded.");
        }

        WriteCharacter();

        CreateBackup(
            filename);

        File.WriteAllBytes(
            filename,
            bytes);

        Filename = filename;
    }

    private static void CreateBackup(
        string filename)
    {
        if (!File.Exists(filename))
            return;

        File.Copy(
            filename,
            filename + ".bak",
            overwrite: true);
    }

    private ushort ReadUInt16(
        int offset)
    {
        return BinaryPrimitives
            .ReadUInt16LittleEndian(
                bytes.AsSpan(
                    offset,
                    2));
    }

    private void WriteUInt16(
        int offset,
        ushort value)
    {
        BinaryPrimitives
            .WriteUInt16LittleEndian(
                bytes.AsSpan(
                    offset,
                    2),
                value);
    }

    private string ReadName()
    {
        int count = 0;

        while (count < NameLength &&
               bytes[NameOffset + count] != 0)
        {
            count++;
        }

        return Encoding.ASCII.GetString(
            bytes,
            NameOffset,
            count);
    }

    private void WriteName(
        string name)
    {
        Array.Clear(
            bytes,
            NameOffset,
            NameLength);

        byte[] encoded =
            Encoding.ASCII.GetBytes(
                name);

        //
        // Leave room for the null terminator.
        //
        int count =
            Math.Min(
                encoded.Length,
                14);

        Array.Copy(
            encoded,
            0,
            bytes,
            NameOffset,
            count);
    }

    private void ReadCharacter()
    {
        character.Name =
            ReadName();

        character.Race =
            (RaceType)
                ReadUInt16(
                    RaceOffset);

        character.Class =
            (ClassType)
                ReadUInt16(
                    ClassOffset);

        character.Sex =
            (SexType)
                ReadUInt16(
                    SexOffset);

        character.HitPoints =
            ReadUInt16(
                HitPointsOffset);

        character.Strength =
            ReadUInt16(
                StrengthOffset);

        character.Agility =
            ReadUInt16(
                AgilityOffset);

        character.Stamina =
            ReadUInt16(
                StaminaOffset);

        character.Charisma =
            ReadUInt16(
                CharismaOffset);

        character.Wisdom =
            ReadUInt16(
                WisdomOffset);

        character.Intelligence =
            ReadUInt16(
                IntelligenceOffset);

        character.Experience =
            ReadUInt16(
                ExperienceOffset);
    }


    private void WriteCharacter()
    {
        WriteName(
            character.Name);

        WriteUInt16(
            RaceOffset,
            (ushort)character.Race);

        WriteUInt16(
            ClassOffset,
            (ushort)character.Class);

        WriteUInt16(
            SexOffset,
            (ushort)character.Sex);

        WriteUInt16(
            HitPointsOffset,
            character.HitPoints);

        WriteUInt16(
            StrengthOffset,
            character.Strength);

        WriteUInt16(
            AgilityOffset,
            character.Agility);

        WriteUInt16(
            StaminaOffset,
            character.Stamina);

        WriteUInt16(
            CharismaOffset,
            character.Charisma);

        WriteUInt16(
            WisdomOffset,
            character.Wisdom);

        WriteUInt16(
            IntelligenceOffset,
            character.Intelligence);

        WriteUInt16(
            ExperienceOffset,
            character.Experience);
    }
}