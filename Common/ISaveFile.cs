namespace UltimaSaveEditor.Common;

public interface ISaveFile
{
    string? Filename { get; }

    bool IsLoaded { get; }

    void Load(string filename);

    void Save();

    void SaveAs(string filename);
}