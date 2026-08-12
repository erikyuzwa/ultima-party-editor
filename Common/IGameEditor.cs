namespace UltimaSaveEditor.Common;

public interface IGameEditor
{
    string GameName { get; }

    string? Filename { get; }

    bool IsLoaded { get; }

    void OpenSave(string filename);

    void Save();

    void SaveAs(string filename);
}