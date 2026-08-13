using System.Buffers.Binary;
using System.Text;
using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima5;

public sealed class Ultima5SaveFile
    : ISaveFile
{
    public const int FileSize =
        4192;

    public const int CharacterCount =
        16;

    private const int CharacterBaseOffset =
        0x0002;

    private const int CharacterRecordSize =
        0x20;

    private const int HelmOffset =
    0x021A;

    private const int ShieldOffset =
        0x021E;

    private const int ArmorOffset =
        0x0223;

    private const int WeaponOffset =
        0x022A;

    private const int RingOffset =
        0x0244;

    private const int AmuletOffset =
        0x0247;

    private const int SpellOffset =
        0x024A;

    private const int ScrollOffset =
        0x027A;

    private const int PotionOffset =
        0x0282;

    private const int ReagentOffset =
        0x02AA;

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
        bytes.Length == FileSize;

    private byte ReadQuantity(
    int baseOffset,
    int index)
    {
        return bytes[
            baseOffset +
            index];
    }

    private void WriteQuantity(
        int baseOffset,
        int index,
        byte quantity)
    {
        bytes[
            baseOffset +
            index] =
            (byte)Math.Min(
                quantity,
                (byte)99);
    }

    public byte GetHelmQuantity(
    HelmType item)
    {
        return ReadQuantity(
            HelmOffset,
            (int)item);
    }

    public void SetHelmQuantity(
        HelmType item,
        byte quantity)
    {
        WriteQuantity(
            HelmOffset,
            (int)item,
            quantity);
    }

    public byte GetShieldQuantity(
        ShieldType item)
    {
        return ReadQuantity(
            ShieldOffset,
            (int)item);
    }

    public void SetShieldQuantity(
        ShieldType item,
        byte quantity)
    {
        WriteQuantity(
            ShieldOffset,
            (int)item,
            quantity);
    }

    public byte GetArmorQuantity(
        ArmorType item)
    {
        return ReadQuantity(
            ArmorOffset,
            (int)item);
    }

    public void SetArmorQuantity(
        ArmorType item,
        byte quantity)
    {
        WriteQuantity(
            ArmorOffset,
            (int)item,
            quantity);
    }

    public byte GetWeaponQuantity(
        WeaponType item)
    {
        return ReadQuantity(
            WeaponOffset,
            (int)item);
    }

    public void SetWeaponQuantity(
        WeaponType item,
        byte quantity)
    {
        WriteQuantity(
            WeaponOffset,
            (int)item,
            quantity);
    }

    public byte GetRingQuantity(
    RingType item)
    {
        return ReadQuantity(
            RingOffset,
            (int)item);
    }

    public void SetRingQuantity(
        RingType item,
        byte quantity)
    {
        WriteQuantity(
            RingOffset,
            (int)item,
            quantity);
    }

    public byte GetAmuletQuantity(
        AmuletType item)
    {
        return ReadQuantity(
            AmuletOffset,
            (int)item);
    }

    public void SetAmuletQuantity(
        AmuletType item,
        byte quantity)
    {
        WriteQuantity(
            AmuletOffset,
            (int)item,
            quantity);
    }

    public byte GetSpellQuantity(
    SpellType item)
    {
        return ReadQuantity(
            SpellOffset,
            (int)item);
    }

    public void SetSpellQuantity(
        SpellType item,
        byte quantity)
    {
        WriteQuantity(
            SpellOffset,
            (int)item,
            quantity);
    }

    public byte GetScrollQuantity(
        ScrollType item)
    {
        return ReadQuantity(
            ScrollOffset,
            (int)item);
    }

    public void SetScrollQuantity(
        ScrollType item,
        byte quantity)
    {
        WriteQuantity(
            ScrollOffset,
            (int)item,
            quantity);
    }

    public byte GetPotionQuantity(
        PotionType item)
    {
        return ReadQuantity(
            PotionOffset,
            (int)item);
    }

    public void SetPotionQuantity(
        PotionType item,
        byte quantity)
    {
        WriteQuantity(
            PotionOffset,
            (int)item,
            quantity);
    }

    public byte GetReagentQuantity(
        ReagentType item)
    {
        return ReadQuantity(
            ReagentOffset,
            (int)item);
    }

    public void SetReagentQuantity(
        ReagentType item,
        byte quantity)
    {
        WriteQuantity(
            ReagentOffset,
            (int)item,
            quantity);
    }

    public Ultima5SaveFile()
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

    public void Load(
        string filename)
    {
        byte[] data =
            File.ReadAllBytes(
                filename);

        if (data.Length != FileSize)
        {
            throw new InvalidDataException(
                $"Invalid Ultima V SAVED file. " +
                $"Expected {FileSize} bytes, " +
                $"found {data.Length}.");
        }

        bytes =
            data;

        Filename =
            filename;

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
                "No Ultima V SAVED file is loaded.");
        }

        if (string.IsNullOrWhiteSpace(
                Filename))
        {
            throw new InvalidOperationException(
                "No filename is available.");
        }

        WriteCharacters();

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
                "No Ultima V SAVED file is loaded.");
        }

        WriteCharacters();

        CreateBackup(
            filename);

        File.WriteAllBytes(
            filename,
            bytes);

        Filename =
            filename;
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

        //
        // Leave room for NULL terminator.
        //
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
                9);

        character.Sex =
            (SexType)
                bytes[offset + 0x09];

        character.Class =
            (ClassType)
                bytes[offset + 0x0A];

        character.Health =
            (HealthStatus)
                bytes[offset + 0x0B];

        character.Strength =
            bytes[offset + 0x0C];

        character.Dexterity =
            bytes[offset + 0x0D];

        character.Intelligence =
            bytes[offset + 0x0E];

        character.MagicPoints =
            bytes[offset + 0x0F];

        character.HitPoints =
            ReadUInt16(
                offset + 0x10);

        character.MaxHitPoints =
            ReadUInt16(
                offset + 0x12);

        character.Experience =
            ReadUInt16(
                offset + 0x14);

        character.Level =
            bytes[offset + 0x16];

        character.Helm =
            (EquipmentItemType)
                bytes[offset + 0x19];

        character.Shield =
            (EquipmentItemType)
                bytes[offset + 0x1A];

        character.Armor =
            (EquipmentItemType)
                bytes[offset + 0x1B];

        character.Weapon =
            (EquipmentItemType)
                bytes[offset + 0x1C];

        character.Ring =
            (EquipmentItemType)
                bytes[offset + 0x1D];

        character.Amulet =
            (EquipmentItemType)
                bytes[offset + 0x1E];
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
            9,
            character.Name);

        bytes[offset + 0x09] =
            (byte)character.Sex;

        bytes[offset + 0x0A] =
            (byte)character.Class;

        bytes[offset + 0x0B] =
            (byte)character.Health;

        bytes[offset + 0x0C] =
            character.Strength;

        bytes[offset + 0x0D] =
            character.Dexterity;

        bytes[offset + 0x0E] =
            character.Intelligence;

        bytes[offset + 0x0F] =
            character.MagicPoints;

        WriteUInt16(
            offset + 0x10,
            character.HitPoints);

        WriteUInt16(
            offset + 0x12,
            character.MaxHitPoints);

        WriteUInt16(
            offset + 0x14,
            character.Experience);

        bytes[offset + 0x16] =
            character.Level;

        bytes[offset + 0x19] =
            (byte)character.Helm;

        bytes[offset + 0x1A] =
            (byte)character.Shield;

        bytes[offset + 0x1B] =
            (byte)character.Armor;

        bytes[offset + 0x1C] =
            (byte)character.Weapon;

        bytes[offset + 0x1D] =
            (byte)character.Ring;

        bytes[offset + 0x1E] =
            (byte)character.Amulet;
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
}
