using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima1;

public sealed class Ultima1EditorControl
    : UserControl,
      IGameEditor
{
    private readonly Ultima1SaveFile save =
        new();

    private readonly CharacterPanel
        characterPanel;

    private readonly EquipmentPanel
    equipmentPanel;

    public string GameName =>
        "Ultima I";

    public string? Filename =>
        save.Filename;

    public bool IsLoaded =>
        save.IsLoaded;

    public Ultima1EditorControl()
    {
        Dock =
        DockStyle.Fill;

        var tabs =
            new TabControl
            {
                Dock =
                    DockStyle.Fill
            };

        var statsPage =
            new TabPage("Stats")
            {
                Padding =
                    new Padding(15)
            };

        var equipmentPage =
            new TabPage("Equipment")
            {
                Padding =
                    new Padding(15)
            };

        characterPanel =
            new CharacterPanel
            {
                Dock =
                    DockStyle.Fill
            };

        equipmentPanel =
            new EquipmentPanel
            {
                Dock =
                    DockStyle.Fill
            };

        statsPage.Controls.Add(
            characterPanel);

        equipmentPage.Controls.Add(
            equipmentPanel);

        tabs.TabPages.Add(
            statsPage);

        tabs.TabPages.Add(
            equipmentPage);

        Controls.Add(
            tabs);
    }

    public void OpenSave(
        string filename)
    {
        save.Load(
            filename);

        characterPanel.LoadFromSave(
            save);

        equipmentPanel.LoadFromSave(
       save);
    }

    public void Save()
    {
        StoreToSave();

        save.Save();
    }

    public void SaveAs(
        string filename)
    {
        StoreToSave();

        save.SaveAs(
            filename);
    }

    private void StoreToSave()
    {
        characterPanel
            .StoreToSave();

        equipmentPanel
        .StoreToSave();
    }
}