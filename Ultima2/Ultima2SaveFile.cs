using System.Text;
using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima2;

public sealed class Ultima2SaveFile
    : ISaveFile
{
    private const int MinimumFileSize =
        0x24;

    private const int NameOffset =
        0x00;

    private const int NameLength =
        12;

    private const int SexOffset =
        0x10;

    private const int RaceOffset =
        0x11;

    private const int ClassOffset =
        0x12;

    private const int StrengthOffset =
        0x15;

    private const int AgilityOffset =
        0x16;

    private const int StaminaOffset =
        0x17;

    private const int CharismaOffset =
        0x18;

    private const int WisdomOffset =
        0x19;

    private const int IntelligenceOffset =
        0x1A;

    private const int HitPointsOffset =
        0x1B;

    private const int ExperienceOffset =
        0x20;

    private const int FoodOffset =
    0x1D;

    private const int GoldOffset =
        0x22;

    private const int TorchesOffset =
        0x2E;

    private const int KeysOffset =
        0x2F;

    private const int ToolsOffset =
        0x30;

    private const int WeaponOffset =
        0x41;

    private const int ArmorOffset =
        0x61;

    private const int SpellOffset =
        0x81;

    private const int GemsOffset =
        0xA6;

    private const int RedGemsOffset =
        0xA8;

    private const int SkullKeysOffset =
        0xA9;

    private const int GreenGemsOffset =
        0xAA;

    private const int StrangeCoinsOffset =
        0xAD;

    private const int RingsOffset =
    0xA0;

    private const int WandsOffset =
        0xA1;

    private const int StaffsOffset =
        0xA2;

    private const int BootsOffset =
        0xA3;

    private const int CloaksOffset =
        0xA4;

    private const int HelmsOffset =
        0xA5;

    private const int AnkhsOffset =
        0xA7;

    private const int BrassButtonsOffset =
        0xAB;

    private const int BlueTasslesOffset =
        0xAC;

    private const int GreenIdolsOffset =
        0xAE;

    private const int TriLithiumsOffset =
        0xAF;

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
        bytes.Length >= MinimumFileSize;

    public PartyCharacter Character =>
        character;

    public ushort Food
    {
        get =>
            ReadBcd16(
                FoodOffset);

        set =>
            WriteBcd16(
                FoodOffset,
                value);
    }

    public ushort Gold
    {
        get =>
            ReadBcd16(
                GoldOffset);

        set =>
            WriteBcd16(
                GoldOffset,
                value);
    }

    public byte Torches
    {
        get =>
            DecodeBcdByte(
                bytes[TorchesOffset]);

        set =>
            bytes[TorchesOffset] =
                EncodeBcdByte(value);
    }

    public byte Keys
    {
        get =>
            DecodeBcdByte(
                bytes[KeysOffset]);

        set =>
            bytes[KeysOffset] =
                EncodeBcdByte(value);
    }

    public byte Tools
    {
        get =>
            DecodeBcdByte(
                bytes[ToolsOffset]);

        set =>
            bytes[ToolsOffset] =
                EncodeBcdByte(value);
    }

    public byte Gems
    {
        get =>
            DecodeBcdByte(
                bytes[GemsOffset]);

        set =>
            bytes[GemsOffset] =
                EncodeBcdByte(value);
    }

    public byte RedGems
    {
        get =>
            DecodeBcdByte(
                bytes[RedGemsOffset]);

        set =>
            bytes[RedGemsOffset] =
                EncodeBcdByte(value);
    }

    public byte SkullKeys
    {
        get =>
            DecodeBcdByte(
                bytes[SkullKeysOffset]);

        set =>
            bytes[SkullKeysOffset] =
                EncodeBcdByte(value);
    }

    public byte GreenGems
    {
        get =>
            DecodeBcdByte(
                bytes[GreenGemsOffset]);

        set =>
            bytes[GreenGemsOffset] =
                EncodeBcdByte(value);
    }

    public byte StrangeCoins
    {
        get =>
            DecodeBcdByte(
                bytes[StrangeCoinsOffset]);

        set =>
            bytes[StrangeCoinsOffset] =
                EncodeBcdByte(value);
    }

    public byte Rings
    {
        get =>
            DecodeBcdByte(
                bytes[RingsOffset]);

        set =>
            bytes[RingsOffset] =
                EncodeBcdByte(value);
    }

    public byte Wands
    {
        get =>
            DecodeBcdByte(
                bytes[WandsOffset]);

        set =>
            bytes[WandsOffset] =
                EncodeBcdByte(value);
    }

    public byte Staffs
    {
        get =>
            DecodeBcdByte(
                bytes[StaffsOffset]);

        set =>
            bytes[StaffsOffset] =
                EncodeBcdByte(value);
    }

    public byte Boots
    {
        get =>
            DecodeBcdByte(
                bytes[BootsOffset]);

        set =>
            bytes[BootsOffset] =
                EncodeBcdByte(value);
    }

    public byte Cloaks
    {
        get =>
            DecodeBcdByte(
                bytes[CloaksOffset]);

        set =>
            bytes[CloaksOffset] =
                EncodeBcdByte(value);
    }

    public byte Helms
    {
        get =>
            DecodeBcdByte(
                bytes[HelmsOffset]);

        set =>
            bytes[HelmsOffset] =
                EncodeBcdByte(value);
    }

    public byte Ankhs
    {
        get =>
            DecodeBcdByte(
                bytes[AnkhsOffset]);

        set =>
            bytes[AnkhsOffset] =
                EncodeBcdByte(value);
    }

    public byte BrassButtons
    {
        get =>
            DecodeBcdByte(
                bytes[BrassButtonsOffset]);

        set =>
            bytes[BrassButtonsOffset] =
                EncodeBcdByte(value);
    }

    public byte BlueTassles
    {
        get =>
            DecodeBcdByte(
                bytes[BlueTasslesOffset]);

        set =>
            bytes[BlueTasslesOffset] =
                EncodeBcdByte(value);
    }

    public byte GreenIdols
    {
        get =>
            DecodeBcdByte(
                bytes[GreenIdolsOffset]);

        set =>
            bytes[GreenIdolsOffset] =
                EncodeBcdByte(value);
    }

    public byte TriLithiums
    {
        get =>
            DecodeBcdByte(
                bytes[TriLithiumsOffset]);

        set =>
            bytes[TriLithiumsOffset] =
                EncodeBcdByte(value);
    }

    public byte GetWeaponQuantity(
    WeaponType weapon)
    {
        return DecodeBcdByte(
            bytes[
                WeaponOffset +
                (int)weapon]);
    }

    public void SetWeaponQuantity(
        WeaponType weapon,
        byte quantity)
    {
        bytes[
            WeaponOffset +
            (int)weapon] =
            EncodeBcdByte(
                quantity);
    }

    public byte GetArmorQuantity(
        ArmorType armor)
    {
        return DecodeBcdByte(
            bytes[
                ArmorOffset +
                (int)armor]);
    }

    public void SetArmorQuantity(
        ArmorType armor,
        byte quantity)
    {
        bytes[
            ArmorOffset +
            (int)armor] =
            EncodeBcdByte(
                quantity);
    }

    public byte GetSpellQuantity(
        SpellType spell)
    {
        return DecodeBcdByte(
            bytes[
                SpellOffset +
                (int)spell]);
    }

    public void SetSpellQuantity(
        SpellType spell,
        byte quantity)
    {
        bytes[
            SpellOffset +
            (int)spell] =
            EncodeBcdByte(
                quantity);
    }

    public void Load(
        string filename)
    {
        byte[] data =
            File.ReadAllBytes(filename);

        if (data.Length <
            MinimumFileSize)
        {
            throw new InvalidDataException(
                "This does not appear to be a valid " +
                "Ultima II PLAYER file.");
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
                "No PLAYER file is loaded.");
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
                "No PLAYER file is loaded.");
        }

        WriteCharacter();

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

        return (byte)(
            ((value / 10) << 4) |
            (value % 10));
    }

    private ushort ReadBcd16(
        int offset)
    {
        int first =
            DecodeBcdByte(
                bytes[offset]);

        int second =
            DecodeBcdByte(
                bytes[offset + 1]);

        return (ushort)(
            first * 100 +
            second);
    }

    private void WriteBcd16(
        int offset,
        ushort value)
    {
        int bounded =
            Math.Min(
                (int)value,
                9999);

        bytes[offset] =
            EncodeBcdByte(
                bounded / 100);

        bytes[offset + 1] =
            EncodeBcdByte(
                bounded % 100);
    }

    private string ReadName()
    {
        StringBuilder result =
            new();

        for (int i = 0;
             i < NameLength;
             i++)
        {
            byte value =
                bytes[
                    NameOffset + i];

            if (value == 0)
                break;

            //
            // Ultima II stores the character
            // name with the high bit set.
            //
            value &=
                0x7F;

            if (value == 0)
                break;

            result.Append(
                (char)value);
        }

        return result.ToString();
    }

    private void WriteName(
        string name)
    {
        //
        // Clear the name area and the following
        // documented terminator area.
        //
        Array.Clear(
            bytes,
            0x00,
            0x10);

        int count =
            Math.Min(
                name.Length,
                NameLength);

        for (int i = 0;
             i < count;
             i++)
        {
            char c =
                name[i];

            byte ascii =
                (byte)(
                    c & 0x7F);

            bytes[
                NameOffset + i] =
                (byte)(
                    ascii | 0x80);
        }
    }

    private void ReadCharacter()
    {
        character.Name =
            ReadName();

        character.Sex =
            (SexType)
                bytes[SexOffset];

        character.Race =
            (RaceType)
                bytes[RaceOffset];

        character.Class =
            (ClassType)
                bytes[ClassOffset];

        character.Strength =
            DecodeBcdByte(
                bytes[StrengthOffset]);

        character.Agility =
            DecodeBcdByte(
                bytes[AgilityOffset]);

        character.Stamina =
            DecodeBcdByte(
                bytes[StaminaOffset]);

        character.Charisma =
            DecodeBcdByte(
                bytes[CharismaOffset]);

        character.Wisdom =
            DecodeBcdByte(
                bytes[WisdomOffset]);

        character.Intelligence =
            DecodeBcdByte(
                bytes[IntelligenceOffset]);

        character.HitPoints =
            ReadBcd16(
                HitPointsOffset);

        character.Experience =
            ReadBcd16(
                ExperienceOffset);
    }

    private void WriteCharacter()
    {
        WriteName(
            character.Name);

        bytes[SexOffset] =
            (byte)character.Sex;

        bytes[RaceOffset] =
            (byte)character.Race;

        bytes[ClassOffset] =
            (byte)character.Class;

        bytes[StrengthOffset] =
            EncodeBcdByte(
                character.Strength);

        bytes[AgilityOffset] =
            EncodeBcdByte(
                character.Agility);

        bytes[StaminaOffset] =
            EncodeBcdByte(
                character.Stamina);

        bytes[CharismaOffset] =
            EncodeBcdByte(
                character.Charisma);

        bytes[WisdomOffset] =
            EncodeBcdByte(
                character.Wisdom);

        bytes[IntelligenceOffset] =
            EncodeBcdByte(
                character.Intelligence);

        WriteBcd16(
            HitPointsOffset,
            character.HitPoints);

        WriteBcd16(
            ExperienceOffset,
            character.Experience);
    }
}