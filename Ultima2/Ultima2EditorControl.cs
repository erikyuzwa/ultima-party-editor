using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima2;

public sealed class Ultima2EditorControl
    : UserControl,
      IGameEditor
{
    private readonly Ultima2SaveFile save =
        new();

    private readonly CharacterPanel characterPanel;

    private readonly EquipmentPanel equipmentPanel;

    private readonly ItemsPanel itemsPanel;

    public string GameName =>
        "Ultima II";

    public string? Filename =>
        save.Filename;

    public bool IsLoaded =>
        save.IsLoaded;

    public Ultima2EditorControl()
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

        var itemsPage =
            new TabPage("Items")
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

        itemsPanel =
    new ItemsPanel
    {
        Dock =
            DockStyle.Fill
    };

        statsPage.Controls.Add(
            characterPanel);

        equipmentPage.Controls.Add(
            equipmentPanel);

        itemsPage.Controls.Add(
    itemsPanel);

        tabs.TabPages.Add(
            statsPage);

        tabs.TabPages.Add(
            equipmentPage);

        tabs.TabPages.Add(
    itemsPage);

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

        itemsPanel.LoadFromSave(
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

        itemsPanel
        .StoreToSave();
    }
}