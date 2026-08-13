
using UltimaSaveEditor.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace UltimaSaveEditor.Ultima4;

public sealed class EquipmentPanel
    : UserControl
{
    private NumericUpDown foodNumeric = null!;
    private NumericUpDown goldNumeric = null!;

    private NumericUpDown torchesNumeric = null!;
    private NumericUpDown keysNumeric = null!;
    private NumericUpDown gemsNumeric = null!;
    private NumericUpDown sextantsNumeric = null!;

    private ComboBox reagentCombo = null!;
    private NumericUpDown reagentQuantityNumeric = null!;

    private ComboBox mixtureCombo = null!;
    private NumericUpDown mixtureQuantityNumeric = null!;

    private Ultima4SaveFile? save;

    private int currentReagentIndex = -1;
    private int currentMixtureIndex = -1;

    private bool loadingControls;

    public EquipmentPanel()
    {
        Padding = new Padding(20);

        //
        // Main two-column layout.
        //
        var mainLayout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 300,

                ColumnCount = 2,
                RowCount = 1,

                Padding = new Padding(0)
            };

        mainLayout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                45));

        mainLayout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                55));

        //
        // Utility
        //
        var utilityGroup =
            new GroupBox
            {
                Text = "Utility",
                Dock = DockStyle.Fill,
                Margin = new Padding(
                    0,
                    0,
                    10,
                    0)
            };

        //
        // Spell inventory
        //
        var spellGroup =
            new GroupBox
            {
                Text = "Spell Mixtures and Reagents",
                Dock = DockStyle.Fill,
                Margin = new Padding(
                    10,
                    0,
                    0,
                    0)
            };

        BuildUtilityGroup(
            utilityGroup);

        BuildSpellGroup(
            spellGroup);

        mainLayout.Controls.Add(
            utilityGroup,
            0,
            0);

        mainLayout.Controls.Add(
            spellGroup,
            1,
            0);

        Controls.Add(
            mainLayout);

        reagentCombo.SelectedIndexChanged +=
            ReagentCombo_SelectedIndexChanged;

        mixtureCombo.SelectedIndexChanged +=
            MixtureCombo_SelectedIndexChanged;
    }

    private void BuildUtilityGroup(
    GroupBox group)
    {
        AddLabel(
            group,
            "Food:",
            25,
            40);

        foodNumeric =
            CreateNumber(
                130,
                35,
                uint.MaxValue);

        AddLabel(
            group,
            "Gold:",
            25,
            75);

        goldNumeric =
            CreateNumber(
                130,
                70,
                ushort.MaxValue);

        AddLabel(
            group,
            "Torches:",
            25,
            110);

        torchesNumeric =
            CreateNumber(
                130,
                105,
                ushort.MaxValue);

        AddLabel(
            group,
            "Keys:",
            25,
            145);

        keysNumeric =
            CreateNumber(
                130,
                140,
                ushort.MaxValue);

        AddLabel(
            group,
            "Gems:",
            25,
            180);

        gemsNumeric =
            CreateNumber(
                130,
                175,
                ushort.MaxValue);

        AddLabel(
            group,
            "Sextants:",
            25,
            215);

        sextantsNumeric =
            CreateNumber(
                130,
                210,
                ushort.MaxValue);

        group.Controls.AddRange(
            new Control[]
            {
            foodNumeric,
            goldNumeric,
            torchesNumeric,
            keysNumeric,
            gemsNumeric,
            sextantsNumeric
            });
    }

    private void BuildSpellGroup(
    GroupBox group)
    {
        AddLabel(
            group,
            "Reagent:",
            25,
            40);

        reagentCombo =
            new ComboBox
            {
                Left = 120,
                Top = 35,
                Width = 190,

                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        foreach (ReagentType reagent
                 in Enum.GetValues<ReagentType>())
        {
            reagentCombo.Items.Add(
                ((Enum)(object)reagent)
                    .ToDisplayName());
        }

        AddLabel(
            group,
            "Quantity:",
            25,
            80);

        reagentQuantityNumeric =
            CreateNumber(
                120,
                75,
                ushort.MaxValue);

        AddLabel(
            group,
            "Mixture:",
            25,
            140);

        mixtureCombo =
            new ComboBox
            {
                Left = 120,
                Top = 135,
                Width = 190,

                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        foreach (SpellMixtureType mixture
                 in Enum.GetValues<SpellMixtureType>())
        {
            mixtureCombo.Items.Add(
                ((Enum)(object)mixture)
                    .ToDisplayName());
        }

        AddLabel(
            group,
            "Quantity:",
            25,
            180);

        mixtureQuantityNumeric =
            CreateNumber(
                120,
                175,
                ushort.MaxValue);

        group.Controls.AddRange(
            new Control[]
            {
            reagentCombo,
            reagentQuantityNumeric,

            mixtureCombo,
            mixtureQuantityNumeric
            });
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
        Control parent,
        string text,
        int x,
        int y)
    {
        parent.Controls.Add(
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