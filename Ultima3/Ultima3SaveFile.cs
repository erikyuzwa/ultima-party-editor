using System.Text;
using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima3;

public sealed class Ultima3SaveFile
    : ISaveFile
{
    public const int CharacterCount = 4;

    private const int CharacterBaseOffset =
        0x12;

    private const int CharacterRecordSize =
        0x40;

    // End of fourth record:
    // 0xD2 + 0x40 = 0x112
    private const int MinimumFileSize =
        0x112;

    private byte[] bytes =
        Array.Empty<byte>();

    private readonly PartyCharacter[] characters =
        new PartyCharacter[CharacterCount];

    public string? Filename
    {
        get;
        private set;
    }

    public bool IsLoaded =>
        bytes.Length >= MinimumFileSize;

    public Ultima3SaveFile()
    {
        for (int i = 0;
             i < CharacterCount;
             i++)
        {
            characters[i] =
                new PartyCharacter();
        }
    }

    public PartyCharacter GetCharacter(
        int index)
    {
        if (index < 0 ||
            index >= CharacterCount)
        {
            throw new ArgumentOutOfRangeException(
                nameof(index));
        }

        return characters[index];
    }

    private static byte DecodeBcdByte(
        byte value)
    {
        int tens =
            (value >> 4) & 0x0F;

        int ones =
            value & 0x0F;

        return (byte)(
            tens * 10 +
            ones);
    }

    private static byte EncodeBcdByte(
        int value)
    {
        value =
            Math.Clamp(
                value,
                0,
                99);

        int tens =
            value / 10;

        int ones =
            value % 10;

        return (byte)(
            (tens << 4) |
            ones);
    }

    private ushort ReadBcd16(
        int offset)
    {
        int highPair =
            DecodeBcdByte(
                bytes[offset]);

        int lowPair =
            DecodeBcdByte(
                bytes[offset + 1]);

        return (ushort)(
            highPair * 100 +
            lowPair);
    }

    private void WriteBcd16(
        int offset,
        ushort value)
    {
        int bounded =
            Math.Min(
                (int)value,
                9999);

        int highPair =
            bounded / 100;

        int lowPair =
            bounded % 100;

        bytes[offset] =
            EncodeBcdByte(
                highPair);

        bytes[offset + 1] =
            EncodeBcdByte(
                lowPair);
    }

    private void ReadCharacter(
        int index)
    {
        int offset =
            CharacterBaseOffset +
            index * CharacterRecordSize;

        PartyCharacter character =
            characters[index];

        character.Name =
            ReadFixedString(
                offset + 0x00,
                10);

        character.Health =
            (HealthStatus)
                bytes[offset + 0x11];

        character.Strength =
            DecodeBcdByte(
                bytes[offset + 0x12]);

        character.Dexterity =
            DecodeBcdByte(
                bytes[offset + 0x13]);

        character.Intelligence =
            DecodeBcdByte(
                bytes[offset + 0x14]);

        character.Wisdom =
            DecodeBcdByte(
                bytes[offset + 0x15]);

        character.Race =
            (RaceType)
                bytes[offset + 0x16];

        character.Class =
            (ClassType)
                bytes[offset + 0x17];

        character.Sex =
            (SexType)
                bytes[offset + 0x18];

        character.MagicPoints =
            DecodeBcdByte(
                bytes[offset + 0x19]);

        character.HitPoints =
            ReadBcd16(
                offset + 0x1A);

        character.MaxHitPoints =
            ReadBcd16(
                offset + 0x1C);

        character.Experience =
            ReadBcd16(
                offset + 0x1E);

        character.Armor =
            (ArmorType)
                bytes[offset + 0x28];

        character.Weapon =
            (WeaponType)
                bytes[offset + 0x30];

        byte marksAndCards =
    bytes[offset + 0x0E];

        character.LoveCard =
            (marksAndCards & 0x01) != 0;

        character.SolCard =
            (marksAndCards & 0x02) != 0;

        character.MoonCard =
            (marksAndCards & 0x04) != 0;

        character.DeathCard =
            (marksAndCards & 0x08) != 0;

        character.ForceMark =
            (marksAndCards & 0x10) != 0;

        character.FireMark =
            (marksAndCards & 0x20) != 0;

        character.SnakeMark =
            (marksAndCards & 0x40) != 0;

        character.KingsMark =
            (marksAndCards & 0x80) != 0;

        character.Torches =
            DecodeBcdByte(
                bytes[offset + 0x0F]);

        character.Food =
            ReadBcd16(
                offset + 0x21);

        character.Gold =
            ReadBcd16(
                offset + 0x23);

        character.MagicGems =
            DecodeBcdByte(
                bytes[offset + 0x25]);

        character.SkullKeys =
            DecodeBcdByte(
                bytes[offset + 0x26]);

        character.TimeStopPowder =
            DecodeBcdByte(
                bytes[offset + 0x27]);

        for (int i = 0;
             i < character.ArmorQuantities.Length;
             i++)
        {
            character.ArmorQuantities[i] =
                DecodeBcdByte(
                    bytes[
                        offset +
                        0x29 +
                        i]);
        }

        for (int i = 0;
             i < character.WeaponQuantities.Length;
             i++)
        {
            character.WeaponQuantities[i] =
                DecodeBcdByte(
                    bytes[
                        offset +
                        0x31 +
                        i]);
        }
    }

    private void WriteCharacter(
        int index)
    {
        int offset =
            CharacterBaseOffset +
            index * CharacterRecordSize;

        PartyCharacter character =
            characters[index];

        WriteFixedString(
            offset + 0x00,
            10,
            character.Name);

        bytes[offset + 0x11] =
            (byte)character.Health;

        bytes[offset + 0x12] =
            EncodeBcdByte(
                character.Strength);

        bytes[offset + 0x13] =
            EncodeBcdByte(
                character.Dexterity);

        bytes[offset + 0x14] =
            EncodeBcdByte(
                character.Intelligence);

        bytes[offset + 0x15] =
            EncodeBcdByte(
                character.Wisdom);

        bytes[offset + 0x16] =
            (byte)character.Race;

        bytes[offset + 0x17] =
            (byte)character.Class;

        bytes[offset + 0x18] =
            (byte)character.Sex;

        bytes[offset + 0x19] =
            EncodeBcdByte(
                character.MagicPoints);

        WriteBcd16(
            offset + 0x1A,
            character.HitPoints);

        WriteBcd16(
            offset + 0x1C,
            character.MaxHitPoints);

        WriteBcd16(
            offset + 0x1E,
            character.Experience);

        bytes[offset + 0x28] =
            (byte)character.Armor;

        bytes[offset + 0x30] =
            (byte)character.Weapon;

        byte marksAndCards = 0;

        if (character.LoveCard)
            marksAndCards |= 0x01;

        if (character.SolCard)
            marksAndCards |= 0x02;

        if (character.MoonCard)
            marksAndCards |= 0x04;

        if (character.DeathCard)
            marksAndCards |= 0x08;

        if (character.ForceMark)
            marksAndCards |= 0x10;

        if (character.FireMark)
            marksAndCards |= 0x20;

        if (character.SnakeMark)
            marksAndCards |= 0x40;

        if (character.KingsMark)
            marksAndCards |= 0x80;

        bytes[offset + 0x0E] =
            marksAndCards;

        bytes[offset + 0x0F] =
            EncodeBcdByte(
                character.Torches);

        WriteBcd16(
            offset + 0x21,
            character.Food);

        WriteBcd16(
            offset + 0x23,
            character.Gold);

        bytes[offset + 0x25] =
            EncodeBcdByte(
                character.MagicGems);

        bytes[offset + 0x26] =
            EncodeBcdByte(
                character.SkullKeys);

        bytes[offset + 0x27] =
            EncodeBcdByte(
                character.TimeStopPowder);

        for (int i = 0;
             i < character.ArmorQuantities.Length;
             i++)
        {
            bytes[
                offset +
                0x29 +
                i] =
                EncodeBcdByte(
                    character.ArmorQuantities[i]);
        }

        for (int i = 0;
             i < character.WeaponQuantities.Length;
             i++)
        {
            bytes[
                offset +
                0x31 +
                i] =
                EncodeBcdByte(
                    character.WeaponQuantities[i]);
        }
    }

    private string ReadFixedString(
        int offset,
        int length)
    {
        int count = 0;

        while (count < length &&
               bytes[offset + count] != 0)
        {
            count++;
        }

        return Encoding.ASCII.GetString(
            bytes,
            offset,
            count);
    }

    private void WriteFixedString(
        int offset,
        int length,
        string value)
    {
        Array.Clear(
            bytes,
            offset,
            length);

        byte[] encoded =
            Encoding.ASCII.GetBytes(
                value);

        int count =
            Math.Min(
                encoded.Length,
                length - 1);

        Array.Copy(
            encoded,
            0,
            bytes,
            offset,
            count);
    }


    public void Load(
        string filename)
    {
        byte[] data =
            File.ReadAllBytes(filename);

        if (data.Length < MinimumFileSize)
        {
            throw new InvalidDataException(
                $"Invalid PARTY.ULT file. " +
                $"Expected at least {MinimumFileSize} bytes, " +
                $"found {data.Length}.");
        }

        bytes = data;
        Filename = filename;

        for (int i = 0;
             i < CharacterCount;
             i++)
        {
            ReadCharacter(i);
        }
    }

    public void Save()
    {
        if (!IsLoaded)
        {
            throw new InvalidOperationException(
                "No PARTY.ULT file is loaded.");
        }

        if (string.IsNullOrWhiteSpace(Filename))
        {
            throw new InvalidOperationException(
                "No filename is available.");
        }

        WriteCharacters();

        CreateBackup(Filename);

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
                "No PARTY.ULT file is loaded.");
        }

        WriteCharacters();

        CreateBackup(filename);

        File.WriteAllBytes(
            filename,
            bytes);

        Filename = filename;
    }

    private void WriteCharacters()
    {
        for (int i = 0;
             i < CharacterCount;
             i++)
        {
            WriteCharacter(i);
        }
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

}