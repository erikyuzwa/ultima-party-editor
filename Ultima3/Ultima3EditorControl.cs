using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima3;

public sealed class Ultima3EditorControl
    : UserControl,
      IGameEditor
{
    private readonly Ultima3SaveFile save =
        new();

    private readonly ComboBox characterCombo;

    private readonly CharacterPanel characterPanel;

    private readonly EquipmentPanel equipmentPanel;

    private bool loadingControls;


    public string GameName =>
        "Ultima III";

    public string? Filename =>
        save.Filename;

    public bool IsLoaded =>
        save.IsLoaded;

    public Ultima3EditorControl()
    {
        Dock =
            DockStyle.Fill;

        Padding = new Padding(20);

        var characterGroup =
            new GroupBox
            {
                Text = "Character",
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(15)
            };

        characterCombo =
            new ComboBox
            {
                Left = 20,
                Top = 30,
                Width = 260,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        characterGroup.Controls.Add(
            characterCombo);

        var tabControl =
            new TabControl
            {
                Dock = DockStyle.Fill
            };

        var statsPage =
            new TabPage("Stats")
            {
                Padding = new Padding(15)
            };

        var equipmentPage =
            new TabPage("Equipment")
            {
                Padding = new Padding(15)
            };

        characterPanel =
            new CharacterPanel
            {
                Dock = DockStyle.Fill
            };

        equipmentPanel =
            new EquipmentPanel
            {
                Dock = DockStyle.Fill
            };

        statsPage.Controls.Add(
            characterPanel);

        equipmentPage.Controls.Add(
            equipmentPanel);

        tabControl.TabPages.Add(
            statsPage);

        tabControl.TabPages.Add(
            equipmentPage);

        Controls.Add(
            tabControl);

        Controls.Add(
            characterGroup);

        characterCombo.SelectedIndexChanged +=
            CharacterCombo_SelectedIndexChanged;
    }

    private void LoadCharacterSelector()
    {
        loadingControls = true;

        characterCombo.Items.Clear();

        for (int i = 0;
             i < Ultima3SaveFile.CharacterCount;
             i++)
        {
            PartyCharacter character =
                save.GetCharacter(i);

            string name =
                string.IsNullOrWhiteSpace(
                    character.Name)
                    ? $"Character {i + 1}"
                    : character.Name;

            characterCombo.Items.Add(
                name);
        }

        loadingControls = false;
    }

    private void SelectCharacter(
        int index)
    {
        if (index < 0 ||
            index >= Ultima3SaveFile.CharacterCount)
        {
            return;
        }

        loadingControls = true;

        characterCombo.SelectedIndex =
            index;

        loadingControls = false;

        characterPanel.SelectCharacter(
            index);

        equipmentPanel.SelectCharacter(
            index);
    }

    private void CharacterCombo_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (loadingControls ||
            !save.IsLoaded)
        {
            return;
        }

        int newIndex =
            characterCombo.SelectedIndex;

        if (newIndex < 0)
            return;

        //
        // Commit edits from the previously-selected
        // character before switching.
        //
        characterPanel.StoreCurrentCharacter();

        equipmentPanel.StoreToSave();

        characterPanel.SelectCharacter(
            newIndex);

        equipmentPanel.SelectCharacter(
            newIndex);
    }


    public void OpenSave(
        string filename)
    {
        save.Load(filename);

        LoadCharacterSelector();

        characterPanel.LoadFromSave(
            save);

        equipmentPanel.LoadFromSave(
            save);

        SelectCharacter(0);
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

    private void StoreToSave()
    {
        characterPanel
            .StoreCurrentCharacter();

        equipmentPanel
        .StoreToSave();
    }
}