using System.Buffers.Binary;
using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima4;

public sealed class UltimaIVSaveFile
    : ISaveFile
{
    public const int FileSize =
        0x1F6;

    private const int FoodOffset =
    0x140;

    private const int GoldOffset =
        0x144;

    private byte[] bytes =
        Array.Empty<byte>();

    public string? Filename
    {
        get;
        private set;
    }

    public bool IsLoaded =>
        bytes.Length == FileSize;

    public uint Food
    {
        get =>
            ReadUInt32(
                FoodOffset);

        set =>
            WriteUInt32(
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
}