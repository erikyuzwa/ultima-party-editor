
using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima4;

public sealed class InventoryPanel
    : UserControl
{
    private readonly NumericUpDown foodNumeric;
    private readonly NumericUpDown goldNumeric;

    private readonly NumericUpDown torchesNumeric;
    private readonly NumericUpDown keysNumeric;
    private readonly NumericUpDown gemsNumeric;
    private readonly NumericUpDown sextantsNumeric;

    private readonly ComboBox reagentCombo;
    private readonly NumericUpDown reagentQuantityNumeric;

    private readonly ComboBox mixtureCombo;
    private readonly NumericUpDown mixtureQuantityNumeric;

    private Ultima4SaveFile? save;

    private int currentReagentIndex = -1;
    private int currentMixtureIndex = -1;

    private bool loadingControls;

    public InventoryPanel()
    {
        Dock = DockStyle.Fill;

        AddLabel("Food:", 20, 25);

        foodNumeric =
            CreateNumber(
                140,
                20,
                uint.MaxValue);

        AddLabel("Gold:", 20, 65);

        goldNumeric =
            CreateNumber(
                140,
                60,
                ushort.MaxValue);

        AddLabel("Torches:", 20, 105);

        torchesNumeric =
            CreateNumber(
                140,
                100,
                ushort.MaxValue);

        AddLabel("Keys:", 20, 145);

        keysNumeric =
            CreateNumber(
                140,
                140,
                ushort.MaxValue);

        AddLabel("Gems:", 20, 185);

        gemsNumeric =
            CreateNumber(
                140,
                180,
                ushort.MaxValue);

        AddLabel("Sextants:", 20, 225);

        sextantsNumeric =
            CreateNumber(
                140,
                220,
                ushort.MaxValue);

        AddLabel("Reagent:", 300, 25);

        reagentCombo =
        new ComboBox
        {
            Left = 400,
            Top = 20,
            Width = 100,
            DropDownStyle =
                ComboBoxStyle.DropDownList
        };

        foreach (ReagentType reagent
                 in Enum.GetValues<ReagentType>())
        {
            reagentCombo.Items.Add(
                reagent.ToDisplayName());
        }

        AddLabel(
            "Quantity:",
            540,
            25);

        reagentQuantityNumeric =
            CreateNumber(
                600,
                20,
                ushort.MaxValue);

        AddLabel("Mixture:", 300, 75);

        mixtureCombo =
            new ComboBox
            {
                Left = 400,
                Top = 70,
                Width = 100,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        foreach (SpellMixtureType mixture
         in Enum.GetValues<SpellMixtureType>())
        {
            mixtureCombo.Items.Add(
                mixture.ToDisplayName());
        }

        AddLabel(
            "Quantity:",
            540,
            75);

        mixtureQuantityNumeric =
            CreateNumber(
                600,
                70,
                ushort.MaxValue);

        Controls.AddRange(
            new Control[]
            {
                foodNumeric,
                goldNumeric,
                torchesNumeric,
                keysNumeric,
                gemsNumeric,
                sextantsNumeric,

                reagentCombo,
                reagentQuantityNumeric,

                mixtureCombo,
                mixtureQuantityNumeric
            });

        reagentCombo.SelectedIndexChanged +=
            ReagentCombo_SelectedIndexChanged;

        mixtureCombo.SelectedIndexChanged +=
            MixtureCombo_SelectedIndexChanged;
    }

    private NumericUpDown CreateNumber(
        int x,
        int y,
        decimal maximum)
    {
        return new NumericUpDown
        {
            Left = x,
            Top = y,
            Width = 80,
            Minimum = 0,
            Maximum = maximum,
            ThousandsSeparator = true
        };
    }

    private void AddLabel(
        string text,
        int x,
        int y)
    {
        Controls.Add(
            new Label
            {
                Text = text,
                Left = x,
                Top = y,
                AutoSize = true
            });
    }

    public void LoadFromSave(
        Ultima4SaveFile saveFile)
    {
        save = saveFile;

        loadingControls = true;

        //
        // PARTY.SAV stores food in hundredths.
        // Display normal whole food units.
        //
        foodNumeric.Value =
            save.Food / 100;

        goldNumeric.Value =
            save.Gold;

        torchesNumeric.Value =
            save.Torches;

        keysNumeric.Value =
            save.Keys;

        gemsNumeric.Value =
            save.Gems;

        sextantsNumeric.Value =
            save.Sextants;

        reagentCombo.SelectedIndex = 0;
        currentReagentIndex = 0;

        reagentQuantityNumeric.Value =
            save.GetReagentQuantity(
                ReagentType.SulfurousAsh);

        mixtureCombo.SelectedIndex = 0;
        currentMixtureIndex = 0;

        mixtureQuantityNumeric.Value =
            save.GetSpellMixtureQuantity(
                SpellMixtureType.Awaken);

        loadingControls = false;
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        //
        // Convert displayed whole food units back to
        // the hundredths used by PARTY.SAV.
        //
        save.Food =
            (uint)foodNumeric.Value * 100;

        save.Gold =
            (ushort)goldNumeric.Value;

        save.Torches =
            (ushort)torchesNumeric.Value;

        save.Keys =
            (ushort)keysNumeric.Value;

        save.Gems =
            (ushort)gemsNumeric.Value;

        save.Sextants =
            (ushort)sextantsNumeric.Value;

        StoreCurrentReagent();

        StoreCurrentMixture();
    }

    private void StoreCurrentReagent()
    {
        if (save is null ||
            currentReagentIndex < 0)
        {
            return;
        }

        save.SetReagentQuantity(
            (ReagentType)currentReagentIndex,
            (ushort)
                reagentQuantityNumeric.Value);
    }

    private void StoreCurrentMixture()
    {
        if (save is null ||
            currentMixtureIndex < 0)
        {
            return;
        }

        save.SetSpellMixtureQuantity(
            (SpellMixtureType)currentMixtureIndex,
            (ushort)
                mixtureQuantityNumeric.Value);
    }

    private void ReagentCombo_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (loadingControls ||
            save is null)
        {
            return;
        }

        StoreCurrentReagent();

        int newIndex =
            reagentCombo.SelectedIndex;

        if (newIndex < 0)
            return;

        currentReagentIndex =
            newIndex;

        reagentQuantityNumeric.Value =
            save.GetReagentQuantity(
                (ReagentType)newIndex);
    }

    private void MixtureCombo_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (loadingControls ||
            save is null)
        {
            return;
        }

        StoreCurrentMixture();

        int newIndex =
            mixtureCombo.SelectedIndex;

        if (newIndex < 0)
            return;

        currentMixtureIndex =
            newIndex;

        mixtureQuantityNumeric.Value =
            save.GetSpellMixtureQuantity(
                (SpellMixtureType)newIndex);
    }
}