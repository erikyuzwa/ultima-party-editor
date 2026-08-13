using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima2;

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
    private NumericUpDown torchesNumeric = null!;
    private NumericUpDown gemsNumeric = null!;
    private NumericUpDown redGemsNumeric = null!;
    private NumericUpDown skullKeysNumeric = null!;

    private NumericUpDown coinsNumeric = null!;
    private NumericUpDown strangeCoinsNumeric = null!;
    private NumericUpDown greenGemsNumeric = null!;

    private NumericUpDown keysNumeric = null!;
    private NumericUpDown toolsNumeric = null!;

    private Ultima2SaveFile? save;

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
                Height = 560,

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
                235));

        mainLayout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                310));

        var spellGroup =
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
            spellGroup);

        BuildEquipmentGroup(
            equipmentGroup);

        BuildUtilityGroup(
            utilityGroup);

        mainLayout.Controls.Add(
            spellGroup,
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
                120,
                40,
                200);

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
                120,
                85,
                99);

        group.Controls.Add(
            spellCombo);

        group.Controls.Add(
            spellQuantityNumeric);
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
                120,
                40,
                200);

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
                120,
                80,
                99);

        AddLabel(
            group,
            "Armour:",
            25,
            135);

        armorCombo =
            CreateCombo(
                120,
                130,
                200);

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
                120,
                170,
                99);

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

        int rightLabel = 370;
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

        coinsNumeric =
            CreateNumber(
                rightControl,
                y,
                9999);

        y += 40;

        AddLabel(
            group,
            "Torches:",
            leftLabel,
            y + 5);

        torchesNumeric =
            CreateNumber(
                leftControl,
                y,
                99);

        AddLabel(
            group,
            "Strange Coins:",
            rightLabel,
            y + 5);

        strangeCoinsNumeric =
            CreateNumber(
                rightControl,
                y,
                99);

        y += 40;

        AddLabel(
            group,
            "Gems:",
            leftLabel,
            y + 5);

        gemsNumeric =
            CreateNumber(
                leftControl,
                y,
                99);

        AddLabel(
            group,
            "Green Gems:",
            rightLabel,
            y + 5);

        greenGemsNumeric =
            CreateNumber(
                rightControl,
                y,
                99);

        y += 40;

        AddLabel(
            group,
            "Red Gems:",
            leftLabel,
            y + 5);

        redGemsNumeric =
            CreateNumber(
                leftControl,
                y,
                99);

        AddLabel(
            group,
            "Keys:",
            rightLabel,
            y + 5);

        keysNumeric =
            CreateNumber(
                rightControl,
                y,
                99);

        y += 40;

        AddLabel(
            group,
            "Skull Keys:",
            leftLabel,
            y + 5);

        skullKeysNumeric =
            CreateNumber(
                leftControl,
                y,
                99);

        AddLabel(
            group,
            "Tools:",
            rightLabel,
            y + 5);

        toolsNumeric =
            CreateNumber(
                rightControl,
                y,
                99);

        group.Controls.AddRange(
            new Control[]
            {
            foodNumeric,
            coinsNumeric,

            torchesNumeric,
            strangeCoinsNumeric,

            gemsNumeric,
            greenGemsNumeric,

            redGemsNumeric,
            keysNumeric,

            skullKeysNumeric,
            toolsNumeric
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
        decimal maximum)
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

    public void LoadFromSave(
    Ultima2SaveFile saveFile)
    {
        save =
            saveFile;

        loadingControls =
            true;

        foodNumeric.Value =
            save.Food;

        coinsNumeric.Value =
            save.Gold;

        torchesNumeric.Value =
            save.Torches;

        gemsNumeric.Value =
            save.Gems;

        redGemsNumeric.Value =
            save.RedGems;

        skullKeysNumeric.Value =
            save.SkullKeys;

        strangeCoinsNumeric.Value =
            save.StrangeCoins;

        greenGemsNumeric.Value =
            save.GreenGems;

        keysNumeric.Value =
            save.Keys;

        toolsNumeric.Value =
            save.Tools;

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

            (byte)
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

            (byte)
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

            (byte)
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
                coinsNumeric.Value;

        save.Torches =
            (byte)
                torchesNumeric.Value;

        save.Gems =
            (byte)
                gemsNumeric.Value;

        save.RedGems =
            (byte)
                redGemsNumeric.Value;

        save.SkullKeys =
            (byte)
                skullKeysNumeric.Value;

        save.StrangeCoins =
            (byte)
                strangeCoinsNumeric.Value;

        save.GreenGems =
            (byte)
                greenGemsNumeric.Value;

        save.Keys =
            (byte)
                keysNumeric.Value;

        save.Tools =
            (byte)
                toolsNumeric.Value;
    }
}
