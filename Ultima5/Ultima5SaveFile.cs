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

    private const int GrappleOffset =
    0x0209;

    private const int MagicCarpetOffset =
        0x020A;

    private const int LordBritishAmuletOffset =
    0x020D;

    private const int CrownOffset =
        0x020E;

    private const int SceptreOffset =
        0x020F;

    private const int ShardFalsehoodOffset =
        0x0210;

    private const int ShardHatredOffset =
        0x0211;

    private const int ShardCowardiceOffset =
        0x0212;

    private const int SpyGlassOffset =
        0x0214;

    private const int HmsCapePlansOffset =
        0x0215;

    private const int SextantOffset =
        0x0216;

    private const int PocketWatchOffset =
        0x0217;

    private const int BlackBadgeOffset =
        0x0218;

    private const int SandalwoodBoxOffset =
        0x0219;

    private const int FoodOffset =
    0x0202;

    private const int GoldOffset =
        0x0204;

    private const int KeysOffset =
        0x0206;

    private const int GemsOffset =
        0x0207;

    private const int TorchesOffset =
        0x0208;

    private const int SkullKeysOffset =
        0x020B;

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

    public byte Keys
    {
        get =>
            bytes[KeysOffset];

        set =>
            bytes[KeysOffset] =
                (byte)Math.Min(
                    value,
                    (byte)99);
    }

    public byte Gems
    {
        get =>
            bytes[GemsOffset];

        set =>
            bytes[GemsOffset] =
                (byte)Math.Min(
                    value,
                    (byte)99);
    }

    public byte Torches
    {
        get =>
            bytes[TorchesOffset];

        set =>
            bytes[TorchesOffset] =
                (byte)Math.Min(
                    value,
                    (byte)99);
    }

    public byte SkullKeys
    {
        get =>
            bytes[SkullKeysOffset];

        set =>
            bytes[SkullKeysOffset] =
                (byte)Math.Min(
                    value,
                    (byte)99);
    }

    public bool HasQuestItem(
    QuestItemType item)
    {
        if (!IsLoaded)
            return false;

        return item switch
        {
            QuestItemType.Grapple =>
                ReadQuestFlag(
                    GrappleOffset),

            QuestItemType.Amulet =>
    ReadQuestFlag(
        LordBritishAmuletOffset),

            QuestItemType.Crown =>
                ReadQuestFlag(
                    CrownOffset),

            QuestItemType.MagicCarpet =>
                ReadQuantityFlag(
                    MagicCarpetOffset),

            QuestItemType.PocketWatch =>
                ReadQuestFlag(
                    PocketWatchOffset),

            QuestItemType.Sceptre =>
                ReadQuestFlag(
                    SceptreOffset),

            QuestItemType.ShardOfHatred =>
                ReadQuestFlag(
                    ShardHatredOffset),

            QuestItemType.ShardOfCowardice =>
                ReadQuestFlag(
                    ShardCowardiceOffset),

            QuestItemType.ShardOfFalsehood =>
                ReadQuestFlag(
                    ShardFalsehoodOffset),

            QuestItemType.HmsCapePlans =>
                ReadQuestFlag(
                    HmsCapePlansOffset),

            QuestItemType.Sextant =>
                ReadQuantityFlag(
                    SextantOffset),

            QuestItemType.SpyGlass =>
                ReadQuantityFlag(
                    SpyGlassOffset),

            QuestItemType.BlackBadge =>
                ReadQuestFlag(
                    BlackBadgeOffset),

            QuestItemType.SandalwoodBox =>
                ReadQuestFlag(
                    SandalwoodBoxOffset),

            _ => false
        };
    }

    public void SetQuestItem(
    QuestItemType item,
    bool owned)
    {
        if (!IsLoaded)
            return;

        switch (item)
        {
            case QuestItemType.Grapple:
                WriteQuestFlag(
                    GrappleOffset,
                    owned);
                break;

            case QuestItemType.Amulet:
                WriteQuestFlag(
                    LordBritishAmuletOffset,
                    owned);
                break;

            case QuestItemType.Crown:
                WriteQuestFlag(
                    CrownOffset,
                    owned);
                break;

            case QuestItemType.MagicCarpet:
                WriteQuantityFlag(
                    MagicCarpetOffset,
                    owned);
                break;

            case QuestItemType.PocketWatch:
                WriteQuestFlag(
                    PocketWatchOffset,
                    owned);
                break;

            case QuestItemType.Sceptre:
                WriteQuestFlag(
                    SceptreOffset,
                    owned);
                break;

            case QuestItemType.ShardOfHatred:
                WriteQuestFlag(
                    ShardHatredOffset,
                    owned);
                break;

            case QuestItemType.ShardOfCowardice:
                WriteQuestFlag(
                    ShardCowardiceOffset,
                    owned);
                break;

            case QuestItemType.ShardOfFalsehood:
                WriteQuestFlag(
                    ShardFalsehoodOffset,
                    owned);
                break;

            case QuestItemType.HmsCapePlans:
                WriteQuestFlag(
                    HmsCapePlansOffset,
                    owned);
                break;

            case QuestItemType.Sextant:
                WriteQuantityFlag(
                    SextantOffset,
                    owned);
                break;

            case QuestItemType.SpyGlass:
                WriteQuantityFlag(
                    SpyGlassOffset,
                    owned);
                break;

            case QuestItemType.BlackBadge:
                WriteQuestFlag(
                    BlackBadgeOffset,
                    owned);
                break;

            case QuestItemType.SandalwoodBox:
                WriteQuestFlag(
                    SandalwoodBoxOffset,
                    owned);
                break;
        }
    }

    private bool ReadQuantityFlag(
    int offset)
    {
        return bytes[offset] > 0;
    }

    private void WriteQuantityFlag(
        int offset,
        bool owned)
    {
        if (!owned)
        {
            bytes[offset] = 0;
        }
        else if (bytes[offset] == 0)
        {
            //
            // User checked an item that wasn't present.
            // Give them one.
            //
            bytes[offset] = 1;
        }

        //
        // If it was already > 0, preserve the quantity.
        //
    }

    private bool ReadQuestFlag(
    int offset)
    {
        return bytes[offset] != 0;
    }

    private void WriteQuestFlag(
        int offset,
        bool owned)
    {
        bytes[offset] =
            owned
                ? (byte)0xFF
                : (byte)0x00;
    }

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
