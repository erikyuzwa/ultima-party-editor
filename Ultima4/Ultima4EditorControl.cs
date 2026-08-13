using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima4;

public class Ultima4EditorControl
    : UserControl,
      IGameEditor
{
    private readonly Ultima4SaveFile save = new();

    private readonly CharacterPanel characterPanel;

    private readonly InventoryPanel inventoryPanel;

    private readonly QuestItemsPanel questItemsPanel;

    private readonly VirtueItemsPanel virtueItemsPanel;

    public string? Filename =>
    save.Filename;

    public string GameName =>
        "Ultima IV";

    public bool IsLoaded =>
        save.IsLoaded;

    public Ultima4EditorControl()
    {
        Dock = DockStyle.Fill;

        var tabControl =
            new TabControl
            {
                Dock = DockStyle.Fill
            };

        var characterPage =
            new TabPage("Party");

        var inventoryPage =
            new TabPage("Party Equipment");

        var questItemsPage =
            new TabPage("Quest Items");

        var virtueItemsPage =
            new TabPage(
                "Virtue Items");

        characterPanel =
            new CharacterPanel
            {
                Dock = DockStyle.Fill
            };

        inventoryPanel =
            new InventoryPanel
            {
                Dock = DockStyle.Fill
            };

        questItemsPanel =
            new QuestItemsPanel
            {
                Dock = DockStyle.Fill
            };

        virtueItemsPanel =
            new VirtueItemsPanel
            {
                Dock =
                    DockStyle.Fill
            };

        characterPage.Controls.Add(
            characterPanel);

        inventoryPage.Controls.Add(
            inventoryPanel);

        questItemsPage.Controls.Add(
            questItemsPanel);

        virtueItemsPage.Controls.Add(
            virtueItemsPanel);

        tabControl.TabPages.Add(
            characterPage);

        tabControl.TabPages.Add(
            inventoryPage);

        tabControl.TabPages.Add(
            questItemsPage);

        tabControl.TabPages.Add(
            virtueItemsPage);

        Controls.Add(
            tabControl);
    }

    public void OpenSave(
        string filename)
    {
        save.Load(filename);

        characterPanel.LoadFromSave(save);

        inventoryPanel.LoadFromSave(save);

        questItemsPanel.LoadFromSave(save);

        virtueItemsPanel.LoadFromSave(save);

        LoadFromSave();
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

        save.SaveAs(filename);
    }

    private void LoadFromSave()
    {

    }

    private void StoreToSave()
    {
        characterPanel
        .StoreCurrentCharacter();

        inventoryPanel
        .StoreToSave();

        questItemsPanel
       .StoreToSave();

        virtueItemsPanel
        .StoreToSave();

    }
}