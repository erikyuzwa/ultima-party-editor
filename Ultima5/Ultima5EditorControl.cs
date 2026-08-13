using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima5;

public sealed class Ultima5EditorControl
    : UserControl,
      IGameEditor
{
    private readonly Ultima5SaveFile save =
        new();

    private readonly CharacterPanel
        characterPanel;

    private readonly EquipmentPanel
        equipmentPanel;

    private readonly QuestItemsPanel
        questItemsPanel;

    public string GameName =>
        "Ultima V";

    public string? Filename =>
        save.Filename;

    public bool IsLoaded =>
        save.IsLoaded;

    public Ultima5EditorControl()
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
            new TabPage("Characters")
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

        var questPage =
            new TabPage("Quest Items")
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

        questItemsPanel =
            new QuestItemsPanel
            {
                Dock =
                    DockStyle.Fill
            };

        statsPage.Controls.Add(
            characterPanel);

        equipmentPage.Controls.Add(
            equipmentPanel);

        questPage.Controls.Add(
            questItemsPanel);

        tabs.TabPages.Add(
            statsPage);

        tabs.TabPages.Add(
            equipmentPage);

        tabs.TabPages.Add(
            questPage);

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

        questItemsPanel.LoadFromSave(
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
            .StoreCurrentCharacter();

        equipmentPanel
            .StoreToSave();

        questItemsPanel
            .StoreToSave();
    }
}