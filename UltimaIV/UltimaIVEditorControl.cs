using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima4;

public class UltimaIVEditorControl
    : UserControl,
      IGameEditor
{
    private readonly UltimaIVSaveFile save = new();

    private readonly CharacterPanel characterPanel;

    private readonly NumericUpDown goldNumeric;
    private readonly NumericUpDown foodNumeric;

    public string? Filename =>
    save.Filename;

    public string GameName =>
        "Ultima IV";

    public bool IsLoaded =>
        save.IsLoaded;

    public UltimaIVEditorControl()
    {
        Dock =
            DockStyle.Fill;

        var titleLabel =
            new Label
            {
                Text = "Ultima IV PARTY.SAV",
                AutoSize = true,
                Left = 20,
                Top = 20
            };

        var foodLabel =
            new Label
            {
                Text = "Food:",
                AutoSize = true,
                Left = 20,
                Top = 65
            };

        foodNumeric =
            new NumericUpDown
            {
                Left = 120,
                Top = 60,
                Width = 120,
                Maximum =
                    uint.MaxValue
            };

        var goldLabel =
            new Label
            {
                Text = "Gold:",
                AutoSize = true,
                Left = 20,
                Top = 105
            };

        goldNumeric =
            new NumericUpDown
            {
                Left = 120,
                Top = 100,
                Width = 120,
                Maximum =
                    ushort.MaxValue
            };

        Controls.Add(titleLabel);
        Controls.Add(foodLabel);
        Controls.Add(foodNumeric);
        Controls.Add(goldLabel);
        Controls.Add(goldNumeric);

        characterPanel =
            new CharacterPanel
            {
                Dock =
                    DockStyle.Fill
            };

        Controls.Add(
            characterPanel);
    }

    public void OpenSave(
        string filename)
    {
        save.Load(filename);

        characterPanel.LoadFromSave(save);

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
        foodNumeric.Value =
            save.Food;

        goldNumeric.Value =
            save.Gold;
    }

    private void StoreToSave()
    {
        characterPanel
        .StoreCurrentCharacter();

        save.Food =
            (uint)foodNumeric.Value;

        save.Gold =
            (ushort)goldNumeric.Value;
    }
}