using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima1;

public sealed class EquipmentPanel
    : UserControl
{
    private ComboBox spellCombo = null!;
    private NumericUpDown spellQuantityNumeric = null!;

    private ComboBox armorCombo = null!;
    private NumericUpDown armorQuantityNumeric = null!;

    private ComboBox weaponCombo = null!;
    private NumericUpDown weaponQuantityNumeric = null!;

    private NumericUpDown foodNumeric = null!;
    private NumericUpDown goldNumeric = null!;

    private NumericUpDown redGemNumeric = null!;
    private NumericUpDown blueGemNumeric = null!;
    private NumericUpDown greenGemNumeric = null!;
    private NumericUpDown whiteGemNumeric = null!;

    private Ultima1SaveFile? save;

    private int currentSpellIndex = -1;
    private int currentArmorIndex = -1;
    private int currentWeaponIndex = -1;

    private bool loadingControls;

    public EquipmentPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        BuildLayout();

        spellCombo.SelectedIndexChanged +=
            SpellCombo_SelectedIndexChanged;

        armorCombo.SelectedIndexChanged +=
            ArmorCombo_SelectedIndexChanged;

        weaponCombo.SelectedIndexChanged +=
            WeaponCombo_SelectedIndexChanged;
    }

    private void BuildLayout()
    {
        var mainLayout =
            new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 540,

                ColumnCount = 2,
                RowCount = 2
            };

        mainLayout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        mainLayout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        mainLayout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                230));

        mainLayout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                290));

        var spellsGroup =
            new GroupBox
            {
                Text = "Spells",
                Dock = DockStyle.Fill,

                Margin =
                    new Padding(
                        0,
                        0,
                        10,
                        10)
            };

        var equipmentGroup =
            new GroupBox
            {
                Text = "Weapons & Armour",
                Dock = DockStyle.Fill,

                Margin =
                    new Padding(
                        10,
                        0,
                        0,
                        10)
            };

        var utilityGroup =
            new GroupBox
            {
                Text = "Utility",
                Dock = DockStyle.Fill,

                Margin =
                    new Padding(
                        0,
                        10,
                        0,
                        0)
            };

        mainLayout.SetColumnSpan(
            utilityGroup,
            2);

        BuildSpellGroup(
            spellsGroup);

        BuildEquipmentGroup(
            equipmentGroup);

        BuildUtilityGroup(
            utilityGroup);

        mainLayout.Controls.Add(
            spellsGroup,
            0,
            0);

        mainLayout.Controls.Add(
            equipmentGroup,
            1,
            0);

        mainLayout.Controls.Add(
            utilityGroup,
            0,
            1);

        Controls.Add(
            mainLayout);
    }

    private void BuildSpellGroup(
        GroupBox group)
    {
        AddLabel(
            group,
            "Spell:",
            25,
            45);

        spellCombo =
            CreateCombo(
                125,
                40,
                210);

        foreach (SpellType spell
                 in Enum.GetValues<SpellType>())
        {
            spellCombo.Items.Add(
                ((Enum)(object)spell)
                    .ToDisplayName());
        }

        AddLabel(
            group,
            "Quantity:",
            25,
            90);

        spellQuantityNumeric =
            CreateNumber(
                125,
                85);

        group.Controls.AddRange(
            new Control[]
            {
                spellCombo,
                spellQuantityNumeric
            });
    }

    private void BuildEquipmentGroup(
       GroupBox group)
    {
        AddLabel(
            group,
            "Weapon:",
            25,
            45);

        weaponCombo =
            CreateCombo(
                125,
                40,
                210);

        foreach (WeaponType weapon
                 in Enum.GetValues<WeaponType>())
        {
            weaponCombo.Items.Add(
                ((Enum)(object)weapon)
                    .ToDisplayName());
        }

        AddLabel(
            group,
            "Quantity:",
            25,
            85);

        weaponQuantityNumeric =
            CreateNumber(
                125,
                80);

        AddLabel(
            group,
            "Armour:",
            25,
            135);

        armorCombo =
            CreateCombo(
                125,
                130,
                210);

        foreach (ArmorType armor
                 in Enum.GetValues<ArmorType>())
        {
            armorCombo.Items.Add(
                ((Enum)(object)armor)
                    .ToDisplayName());
        }

        AddLabel(
            group,
            "Quantity:",
            25,
            175);

        armorQuantityNumeric =
            CreateNumber(
                125,
                170);

        group.Controls.AddRange(
            new Control[]
            {
                weaponCombo,
                weaponQuantityNumeric,

                armorCombo,
                armorQuantityNumeric
            });
    }

    private void BuildUtilityGroup(
        GroupBox group)
    {
        int leftLabel = 25;
        int leftControl = 150;

        int rightLabel = 375;
        int rightControl = 500;

        int y = 40;

        AddLabel(
            group,
            "Food:",
            leftLabel,
            y + 5);

        foodNumeric =
            CreateNumber(
                leftControl,
                y,
                9999);

        AddLabel(
            group,
            "Coins:",
            rightLabel,
            y + 5);

        goldNumeric =
            CreateNumber(
                rightControl,
                y,
                9999);

        y += 45;

        AddLabel(
            group,
            "Red Gem:",
            leftLabel,
            y + 5);

        redGemNumeric =
            CreateNumber(
                leftControl,
                y);

        AddLabel(
            group,
            "Blue Gem:",
            rightLabel,
            y + 5);

        blueGemNumeric =
            CreateNumber(
                rightControl,
                y);

        y += 45;

        AddLabel(
            group,
            "Green Gem:",
            leftLabel,
            y + 5);

        greenGemNumeric =
            CreateNumber(
                leftControl,
                y);

        AddLabel(
            group,
            "White Gem:",
            rightLabel,
            y + 5);

        whiteGemNumeric =
            CreateNumber(
                rightControl,
                y);

        group.Controls.AddRange(
            new Control[]
            {
                foodNumeric,
                goldNumeric,

                redGemNumeric,
                blueGemNumeric,
                greenGemNumeric,
                whiteGemNumeric
            });
    }

    private static ComboBox CreateCombo(
        int x,
        int y,
        int width)
    {
        return new ComboBox
        {
            Left = x,
            Top = y,
            Width = width,

            DropDownStyle =
                ComboBoxStyle.DropDownList
        };
    }

    private static NumericUpDown CreateNumber(
        int x,
        int y,
        decimal maximum = 9999)
    {
        return new NumericUpDown
        {
            Left = x,
            Top = y,
            Width = 110,

            Minimum = 0,
            Maximum = maximum,

            ThousandsSeparator = true
        };
    }

    private static void AddLabel(
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

    private void ShowSpellQuantity()
    {
        if (save is null ||
            currentSpellIndex < 0)
        {
            return;
        }

        spellQuantityNumeric.Value =
            save.GetSpellQuantity(
                (SpellType)
                    currentSpellIndex);
    }

    private void ShowWeaponQuantity()
    {
        if (save is null ||
            currentWeaponIndex < 0)
        {
            return;
        }

        weaponQuantityNumeric.Value =
            save.GetWeaponQuantity(
                (WeaponType)
                    currentWeaponIndex);
    }

    private void ShowArmorQuantity()
    {
        if (save is null ||
            currentArmorIndex < 0)
        {
            return;
        }

        armorQuantityNumeric.Value =
            save.GetArmorQuantity(
                (ArmorType)
                    currentArmorIndex);
    }

    public void LoadFromSave(
        Ultima1SaveFile saveFile)
    {
        save =
            saveFile;

        loadingControls =
            true;

        foodNumeric.Value =
            save.Food;

        goldNumeric.Value =
            save.Gold;

        redGemNumeric.Value =
            save.RedGem;

        blueGemNumeric.Value =
            save.BlueGem;

        greenGemNumeric.Value =
            save.GreenGem;

        whiteGemNumeric.Value =
            save.WhiteGem;

        currentSpellIndex = 0;
        currentWeaponIndex = 0;
        currentArmorIndex = 0;

        spellCombo.SelectedIndex =
            0;

        weaponCombo.SelectedIndex =
            0;

        armorCombo.SelectedIndex =
            0;

        ShowSpellQuantity();
        ShowWeaponQuantity();
        ShowArmorQuantity();

        loadingControls =
            false;
    }

    private void StoreCurrentSpell()
    {
        if (save is null ||
            currentSpellIndex < 0)
        {
            return;
        }

        save.SetSpellQuantity(
            (SpellType)
                currentSpellIndex,

            (ushort)
                spellQuantityNumeric.Value);
    }

    private void StoreCurrentWeapon()
    {
        if (save is null ||
            currentWeaponIndex < 0)
        {
            return;
        }

        save.SetWeaponQuantity(
            (WeaponType)
                currentWeaponIndex,

            (ushort)
                weaponQuantityNumeric.Value);
    }

    private void StoreCurrentArmor()
    {
        if (save is null ||
            currentArmorIndex < 0)
        {
            return;
        }

        save.SetArmorQuantity(
            (ArmorType)
                currentArmorIndex,

            (ushort)
                armorQuantityNumeric.Value);
    }


    private void SpellCombo_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (loadingControls ||
            save is null)
        {
            return;
        }

        StoreCurrentSpell();

        currentSpellIndex =
            spellCombo.SelectedIndex;

        ShowSpellQuantity();
    }

    private void WeaponCombo_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (loadingControls ||
            save is null)
        {
            return;
        }

        StoreCurrentWeapon();

        currentWeaponIndex =
            weaponCombo.SelectedIndex;

        ShowWeaponQuantity();
    }

    private void ArmorCombo_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (loadingControls ||
            save is null)
        {
            return;
        }

        StoreCurrentArmor();

        currentArmorIndex =
            armorCombo.SelectedIndex;

        ShowArmorQuantity();
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        StoreCurrentSpell();
        StoreCurrentWeapon();
        StoreCurrentArmor();

        save.Food =
            (ushort)
                foodNumeric.Value;

        save.Gold =
            (ushort)
                goldNumeric.Value;

        save.RedGem =
            (ushort)
                redGemNumeric.Value;

        save.BlueGem =
            (ushort)
                blueGemNumeric.Value;

        save.GreenGem =
            (ushort)
                greenGemNumeric.Value;

        save.WhiteGem =
            (ushort)
                whiteGemNumeric.Value;
    }
}