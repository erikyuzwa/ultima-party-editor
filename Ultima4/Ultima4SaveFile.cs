using System.Buffers.Binary;
using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima4;

public sealed class Ultima4SaveFile
    : ISaveFile
{
    public const int FileSize = 0x1F6;

    private const int FoodOffset = 0x140;
    private const int GoldOffset = 0x144;

    private const int TorchesOffset = 0x156;
    private const int GemsOffset = 0x158;
    private const int KeysOffset = 0x15A;
    private const int SextantsOffset = 0x15C;

    private const int ReagentOffset = 0x18E;
    private const int MixtureOffset = 0x19E;

    private const int CharacterBaseOffset = 0x08;

    private const int CharacterRecordSize = 39;

    private const int QuestItems1Offset = 0x1D2;
    private const int QuestItems2Offset = 0x1D3;

    private const byte SkullBit = 1 << 0;

    private const byte CandleBit = 1 << 2;
    private const byte BookBit = 1 << 3;
    private const byte BellBit = 1 << 4;

    private const byte KeyCourageBit = 1 << 5;
    private const byte KeyLoveBit = 1 << 6;
    private const byte KeyTruthBit = 1 << 7;

    private const byte HornBit = 1 << 0;
    private const byte WheelBit = 1 << 1;

    private const int KarmaOffset = 0x146;

    private const int StonesOffset = 0x1D6;
    private const int RunesOffset = 0x1D7;

    public const int CharacterCount = 8;



    private byte[] bytes = Array.Empty<byte>();

    private readonly PartyCharacter[] characters =
    new PartyCharacter[CharacterCount];

    public Ultima4SaveFile()
    {
        for (int i = 0;
         i < CharacterCount;
         i++)
        {
            characters[i] =
                new PartyCharacter();
        }

    }

    public string? Filename
    {
        get;
        private set;
    }

    public bool IsLoaded =>
        bytes.Length == FileSize;

    public uint Food
    {
        get => ReadUInt32(FoodOffset);
        set => WriteUInt32(FoodOffset, value);
    }

    public ushort Gold
    {
        get => ReadUInt16(GoldOffset);
        set => WriteUInt16(GoldOffset, value);
    }

    public ushort Torches
    {
        get => ReadUInt16(TorchesOffset);
        set => WriteUInt16(TorchesOffset, value);
    }

    public ushort Gems
    {
        get => ReadUInt16(GemsOffset);
        set => WriteUInt16(GemsOffset, value);
    }

    public ushort Keys
    {
        get => ReadUInt16(KeysOffset);
        set => WriteUInt16(KeysOffset, value);
    }

    public ushort Sextants
    {
        get => ReadUInt16(SextantsOffset);
        set => WriteUInt16(SextantsOffset, value);
    }

    public ushort GetReagentQuantity(
        ReagentType reagent)
    {
        return ReadUInt16(
            ReagentOffset +
            ((int)reagent * 2));
    }

    public void SetReagentQuantity(
        ReagentType reagent,
        ushort quantity)
    {
        WriteUInt16(
            ReagentOffset +
            ((int)reagent * 2),
            quantity);
    }

    public ushort GetSpellMixtureQuantity(
        SpellMixtureType spell)
    {
        return ReadUInt16(
            MixtureOffset +
            ((int)spell * 2));
    }

    public void SetSpellMixtureQuantity(
        SpellMixtureType spell,
        ushort quantity)
    {
        WriteUInt16(
            MixtureOffset +
            ((int)spell * 2),
            quantity);
    }

    public bool HasQuestItem(
    QuestItem item)
    {
        return item switch
        {
            QuestItem.MondainsSkull =>
                GetFlag(
                    QuestItems1Offset,
                    SkullBit),

            QuestItem.SilverHorn =>
                GetFlag(
                    QuestItems2Offset,
                    HornBit),

            QuestItem.Wheel =>
                GetFlag(
                    QuestItems2Offset,
                    WheelBit),

            QuestItem.CandleOfLove =>
                GetFlag(
                    QuestItems1Offset,
                    CandleBit),

            QuestItem.BookOfTruth =>
                GetFlag(
                    QuestItems1Offset,
                    BookBit),

            QuestItem.BellOfCourage =>
                GetFlag(
                    QuestItems1Offset,
                    BellBit),

            QuestItem.KeyOfLove =>
                GetFlag(
                    QuestItems1Offset,
                    KeyLoveBit),

            QuestItem.KeyOfTruth =>
                GetFlag(
                    QuestItems1Offset,
                    KeyTruthBit),

            QuestItem.KeyOfCourage =>
                GetFlag(
                    QuestItems1Offset,
                    KeyCourageBit),

            _ => false
        };
    }

    public void SetQuestItem(
        QuestItem item,
        bool owned)
    {
        switch (item)
        {
            case QuestItem.MondainsSkull:
                SetFlag(
                    QuestItems1Offset,
                    SkullBit,
                    owned);
                break;

            case QuestItem.SilverHorn:
                SetFlag(
                    QuestItems2Offset,
                    HornBit,
                    owned);
                break;

            case QuestItem.Wheel:
                SetFlag(
                    QuestItems2Offset,
                    WheelBit,
                    owned);
                break;

            case QuestItem.CandleOfLove:
                SetFlag(
                    QuestItems1Offset,
                    CandleBit,
                    owned);
                break;

            case QuestItem.BookOfTruth:
                SetFlag(
                    QuestItems1Offset,
                    BookBit,
                    owned);
                break;

            case QuestItem.BellOfCourage:
                SetFlag(
                    QuestItems1Offset,
                    BellBit,
                    owned);
                break;

            case QuestItem.KeyOfLove:
                SetFlag(
                    QuestItems1Offset,
                    KeyLoveBit,
                    owned);
                break;

            case QuestItem.KeyOfTruth:
                SetFlag(
                    QuestItems1Offset,
                    KeyTruthBit,
                    owned);
                break;

            case QuestItem.KeyOfCourage:
                SetFlag(
                    QuestItems1Offset,
                    KeyCourageBit,
                    owned);
                break;
        }
    }

    public bool HasStone(
    StoneType stone)
    {
        int bit =
            (int)stone;

        byte mask =
            (byte)(1 << bit);

        return (bytes[StonesOffset] & mask) != 0;
    }

    public void SetStone(
        StoneType stone,
        bool owned)
    {
        int bit =
            (int)stone;

        byte mask =
            (byte)(1 << bit);

        if (owned)
        {
            bytes[StonesOffset] |= mask;
        }
        else
        {
            bytes[StonesOffset] &=
                (byte)~mask;
        }
    }

    public bool HasRune(
        VirtueType virtue)
    {
        int bit =
            (int)virtue;

        byte mask =
            (byte)(1 << bit);

        return (bytes[RunesOffset] & mask) != 0;
    }

    public void SetRune(
        VirtueType virtue,
        bool owned)
    {
        int bit =
            (int)virtue;

        byte mask =
            (byte)(1 << bit);

        if (owned)
        {
            bytes[RunesOffset] |= mask;
        }
        else
        {
            bytes[RunesOffset] &=
                (byte)~mask;
        }
    }

    public ushort GetVirtueValue(
    VirtueType virtue)
    {
        int index =
            (int)virtue;

        return ReadUInt16(
            KarmaOffset +
            index * 2);
    }

    public void SetVirtueValue(
        VirtueType virtue,
        ushort value)
    {
        int index =
            (int)virtue;

        WriteUInt16(
            KarmaOffset +
            index * 2,
            value);
    }

    public void Load(
        string filename)
    {
        byte[] data =
            File.ReadAllBytes(filename);

        if (data.Length != FileSize)
        {
            throw new InvalidDataException(
                $"Invalid PARTY.SAV size. " +
                $"Expected {FileSize} bytes, " +
                $"but found {data.Length}.");
        }

        bytes = data;

        for (int i = 0;
         i < CharacterCount;
         i++)
        {
            ReadCharacter(i);
        }

        Filename = filename;
    }

    public void Save()
    {
        if (!IsLoaded)
        {
            throw new InvalidOperationException(
                "No PARTY.SAV file is loaded.");
        }

        if (string.IsNullOrWhiteSpace(
                Filename))
        {
            throw new InvalidOperationException(
                "The save file does not have a filename.");
        }

        CreateBackup(Filename);

        for (int i = 0;
            i < CharacterCount;
            i++)
        {
            WriteCharacter(i);
        }

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
                "No PARTY.SAV file is loaded.");
        }

        for (int i = 0;
            i < CharacterCount;
            i++)
        {
            WriteCharacter(i);
        }

        CreateBackup(filename);

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

        string backupFilename =
            filename + ".bak";

        File.Copy(
            filename,
            backupFilename,
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

    private uint ReadUInt32(
        int offset)
    {
        return BinaryPrimitives
            .ReadUInt32LittleEndian(
                bytes.AsSpan(
                    offset,
                    4));
    }

    private void WriteUInt32(
        int offset,
        uint value)
    {
        BinaryPrimitives
            .WriteUInt32LittleEndian(
                bytes.AsSpan(
                    offset,
                    4),
                value);
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

        return System.Text.Encoding.ASCII.GetString(
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
            System.Text.Encoding.ASCII.GetBytes(
                value);

        int count =
            Math.Min(
                encoded.Length,
                length);

        Array.Copy(
            encoded,
            0,
            bytes,
            offset,
            count);
    }

    private bool GetFlag(
    int offset,
    byte mask)
    {
        return (bytes[offset] & mask) != 0;
    }

    private void SetFlag(
        int offset,
        byte mask,
        bool enabled)
    {
        if (enabled)
        {
            bytes[offset] |= mask;
        }
        else
        {
            bytes[offset] &=
                (byte)~mask;
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

    private void ReadCharacter(
    int index)
    {
        int offset =
            CharacterBaseOffset +
            index * CharacterRecordSize;

        PartyCharacter character =
            characters[index];

        character.HitPoints =
            ReadUInt16(offset + 0x00);

        character.MaxHitPoints =
            ReadUInt16(offset + 0x02);

        character.Experience =
            ReadUInt16(offset + 0x04);

        character.Strength =
            ReadUInt16(offset + 0x06);

        character.Dexterity =
            ReadUInt16(offset + 0x08);

        character.Intelligence =
            ReadUInt16(offset + 0x0A);

        character.MagicPoints =
            ReadUInt16(offset + 0x0C);

        character.Unknown =
            ReadUInt16(offset + 0x0E);

        character.Weapon =
            (WeaponType)
                ReadUInt16(offset + 0x10);

        character.Armor =
            (ArmorType)
                ReadUInt16(offset + 0x12);

        character.Name =
            ReadFixedString(
                offset + 0x14,
                16);

        character.Sex =
            bytes[offset + 0x24];

        character.ClassType =
            bytes[offset + 0x25];

        character.Status =
            bytes[offset + 0x26];
    }

    private void WriteCharacter(
    int index)
    {
        int offset =
            CharacterBaseOffset +
            index * CharacterRecordSize;

        PartyCharacter character =
            characters[index];

        WriteUInt16(
            offset + 0x00,
            character.HitPoints);

        WriteUInt16(
            offset + 0x02,
            character.MaxHitPoints);

        WriteUInt16(
            offset + 0x04,
            character.Experience);

        WriteUInt16(
            offset + 0x06,
            character.Strength);

        WriteUInt16(
            offset + 0x08,
            character.Dexterity);

        WriteUInt16(
            offset + 0x0A,
            character.Intelligence);

        WriteUInt16(
            offset + 0x0C,
            character.MagicPoints);

        WriteUInt16(
            offset + 0x0E,
            character.Unknown);

        WriteUInt16(
            offset + 0x10,
            (ushort)character.Weapon);

        WriteUInt16(
            offset + 0x12,
            (ushort)character.Armor);

        WriteFixedString(
            offset + 0x14,
            16,
            character.Name);

        bytes[offset + 0x24] =
            character.Sex;

        bytes[offset + 0x25] =
            character.ClassType;

        bytes[offset + 0x26] =
            character.Status;
    }
}