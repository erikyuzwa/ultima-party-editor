using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima3;

public sealed class EquipmentPanel
    : UserControl
{
   
    private readonly ComboBox weaponCombo;
    private readonly NumericUpDown weaponQuantityNumeric;

    private readonly ComboBox armorCombo;
    private readonly NumericUpDown armorQuantityNumeric;

    private readonly NumericUpDown foodNumeric;
    private readonly NumericUpDown goldNumeric;
    private readonly NumericUpDown torchesNumeric;
    private readonly NumericUpDown gemsNumeric;
    private readonly NumericUpDown keysNumeric;
    private readonly NumericUpDown powderNumeric;

    private readonly CheckBox forceMarkCheck;
    private readonly CheckBox fireMarkCheck;
    private readonly CheckBox snakeMarkCheck;
    private readonly CheckBox kingsMarkCheck;

    private readonly CheckBox loveCardCheck;
    private readonly CheckBox solCardCheck;
    private readonly CheckBox moonCardCheck;
    private readonly CheckBox deathCardCheck;

    private Ultima3SaveFile? save;

    private int currentCharacterIndex = -1;
    private int currentWeaponIndex = -1;
    private int currentArmorIndex = -1;

    private bool loadingControls;

    public EquipmentPanel()
    {
        Dock = DockStyle.Fill;

        Padding =
            new Padding(20);


        //
        // Equipment group
        //
        var equipmentGroup =
            new GroupBox
            {
                Text = "Equipment",
                Left = 20,
                Top = 65,
                Width = 480,
                Height = 175
            };

        AddLabel(
            equipmentGroup,
            "Weapon:",
            20,
            40);

        weaponCombo =
            CreateCombo(
                110,
                35,
                190);

        AddLabel(
            equipmentGroup,
            "Quantity:",
            315,
            40);

        weaponQuantityNumeric =
            CreateNumber(
                385,
                35,
                99);

        AddLabel(
            equipmentGroup,
            "Armor:",
            20,
            90);

        armorCombo =
            CreateCombo(
                110,
                85,
                190);

        AddLabel(
            equipmentGroup,
            "Quantity:",
            315,
            90);

        armorQuantityNumeric =
            CreateNumber(
                385,
                85,
                99);

        equipmentGroup.Controls.AddRange(
            new Control[]
            {
                weaponCombo,
                weaponQuantityNumeric,
                armorCombo,
                armorQuantityNumeric
            });

        //
        // Utility group
        //
        var utilityGroup =
            new GroupBox
            {
                Text = "Utility",
                Left = 520,
                Top = 65,
                Width = 360,
                Height = 275
            };

        AddLabel(
            utilityGroup,
            "Food:",
            20,
            40);

        foodNumeric =
            CreateNumber(
                160,
                35,
                9999);

        AddLabel(
            utilityGroup,
            "Gold:",
            20,
            75);

        goldNumeric =
            CreateNumber(
                160,
                70,
                9999);

        AddLabel(
            utilityGroup,
            "Torches:",
            20,
            110);

        torchesNumeric =
            CreateNumber(
                160,
                105,
                99);

        AddLabel(
            utilityGroup,
            "Magic Gems:",
            20,
            145);

        gemsNumeric =
            CreateNumber(
                160,
                140,
                99);

        AddLabel(
            utilityGroup,
            "Skull Keys:",
            20,
            180);

        keysNumeric =
            CreateNumber(
                160,
                175,
                99);

        AddLabel(
            utilityGroup,
            "Time Stop Powder:",
            20,
            215);

        powderNumeric =
            CreateNumber(
                160,
                210,
                99);

        utilityGroup.Controls.AddRange(
            new Control[]
            {
                foodNumeric,
                goldNumeric,
                torchesNumeric,
                gemsNumeric,
                keysNumeric,
                powderNumeric
            });

        //
        // Marks
        //
        var marksGroup =
            new GroupBox
            {
                Text = "Marks",
                Left = 20,
                Top = 260,
                Width = 230,
                Height = 190
            };

        forceMarkCheck =
            CreateCheckBox(
                "Force Mark",
                20,
                35);

        fireMarkCheck =
            CreateCheckBox(
                "Fire Mark",
                20,
                70);

        snakeMarkCheck =
            CreateCheckBox(
                "Snake Mark",
                20,
                105);

        kingsMarkCheck =
            CreateCheckBox(
                "Kings Mark",
                20,
                140);

        marksGroup.Controls.AddRange(
            new Control[]
            {
                forceMarkCheck,
                fireMarkCheck,
                snakeMarkCheck,
                kingsMarkCheck
            });

        //
        // Cards
        //
        var cardsGroup =
            new GroupBox
            {
                Text = "Cards",
                Left = 270,
                Top = 260,
                Width = 230,
                Height = 190
            };

        loveCardCheck =
            CreateCheckBox(
                "Love Card",
                20,
                35);

        solCardCheck =
            CreateCheckBox(
                "Sol Card",
                20,
                70);

        moonCardCheck =
            CreateCheckBox(
                "Moon Card",
                20,
                105);

        deathCardCheck =
            CreateCheckBox(
                "Death Card",
                20,
                140);

        cardsGroup.Controls.AddRange(
            new Control[]
            {
                loveCardCheck,
                solCardCheck,
                moonCardCheck,
                deathCardCheck
            });

        Controls.AddRange(
            new Control[]
            {
                equipmentGroup,
                utilityGroup,
                marksGroup,
                cardsGroup
            });

        PopulateEquipmentCombos();


        weaponCombo.SelectedIndexChanged +=
            WeaponCombo_SelectedIndexChanged;

        armorCombo.SelectedIndexChanged +=
            ArmorCombo_SelectedIndexChanged;
    }

    private void PopulateEquipmentCombos()
    {
        //
        // Inventory starts with Dagger;
        // Hands has no inventory quantity.
        //
        foreach (WeaponType weapon
                 in Enum.GetValues<WeaponType>())
        {
            if (weapon == WeaponType.Hands)
                continue;

            weaponCombo.Items.Add(
                ((Enum)(object)weapon)
                    .ToDisplayName());
        }

        //
        // Inventory starts with Cloth;
        // Skin has no inventory quantity.
        //
        foreach (ArmorType armor
                 in Enum.GetValues<ArmorType>())
        {
            if (armor == ArmorType.Skin)
                continue;

            armorCombo.Items.Add(
                ((Enum)(object)armor)
                    .ToDisplayName());
        }
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
            Width = 100,

            Minimum = 0,
            Maximum = maximum
        };
    }

    private static CheckBox CreateCheckBox(
        string text,
        int x,
        int y)
    {
        return new CheckBox
        {
            Text = text,
            Left = x,
            Top = y,
            AutoSize = true
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

    public void SelectCharacter(
    int index)
    {
        if (save is null)
            return;

        if (index < 0 ||
            index >= Ultima3SaveFile.CharacterCount)
        {
            return;
        }

        if (currentCharacterIndex >= 0 &&
            currentCharacterIndex != index)
        {
            StoreCurrentCharacter();
        }

        currentCharacterIndex =
            index;

        ShowCharacterInventory();
    }

    public void LoadFromSave(
       Ultima3SaveFile saveFile)
    {
        save =
            saveFile;

        currentCharacterIndex =
        -1;

        currentWeaponIndex =
            0;

        currentArmorIndex =
            0;

        loadingControls =
            true;

        weaponCombo.SelectedIndex =
            0;

        armorCombo.SelectedIndex =
            0;

        loadingControls =
            false;
    }



    private void ShowCharacterInventory()
    {
        if (save is null ||
            currentCharacterIndex < 0)
        {
            return;
        }

        loadingControls =
            true;

        PartyCharacter character =
            save.GetCharacter(
                currentCharacterIndex);

        foodNumeric.Value =
            character.Food;

        goldNumeric.Value =
            character.Gold;

        torchesNumeric.Value =
            character.Torches;

        gemsNumeric.Value =
            character.MagicGems;

        keysNumeric.Value =
            character.SkullKeys;

        powderNumeric.Value =
            character.TimeStopPowder;

        forceMarkCheck.Checked =
            character.ForceMark;

        fireMarkCheck.Checked =
            character.FireMark;

        snakeMarkCheck.Checked =
            character.SnakeMark;

        kingsMarkCheck.Checked =
            character.KingsMark;

        loveCardCheck.Checked =
            character.LoveCard;

        solCardCheck.Checked =
            character.SolCard;

        moonCardCheck.Checked =
            character.MoonCard;

        deathCardCheck.Checked =
            character.DeathCard;

        ShowWeaponQuantity();
        ShowArmorQuantity();

        loadingControls =
            false;
    }

    private void ShowWeaponQuantity()
    {
        if (save is null ||
            currentCharacterIndex < 0 ||
            currentWeaponIndex < 0)
        {
            return;
        }

        PartyCharacter character =
            save.GetCharacter(
                currentCharacterIndex);

        weaponQuantityNumeric.Value =
            character.WeaponQuantities[
                currentWeaponIndex];
    }

    private void ShowArmorQuantity()
    {
        if (save is null ||
            currentCharacterIndex < 0 ||
            currentArmorIndex < 0)
        {
            return;
        }

        PartyCharacter character =
            save.GetCharacter(
                currentCharacterIndex);

        armorQuantityNumeric.Value =
            character.ArmorQuantities[
                currentArmorIndex];
    }

    private void StoreCurrentWeaponQuantity()
    {
        if (save is null ||
            currentCharacterIndex < 0 ||
            currentWeaponIndex < 0)
        {
            return;
        }

        save.GetCharacter(
            currentCharacterIndex)
            .WeaponQuantities[
                currentWeaponIndex] =
            (byte)
                weaponQuantityNumeric.Value;
    }

    private void StoreCurrentArmorQuantity()
    {
        if (save is null ||
            currentCharacterIndex < 0 ||
            currentArmorIndex < 0)
        {
            return;
        }

        save.GetCharacter(
            currentCharacterIndex)
            .ArmorQuantities[
                currentArmorIndex] =
            (byte)
                armorQuantityNumeric.Value;
    }

    private void StoreCurrentCharacter()
    {
        if (save is null ||
            currentCharacterIndex < 0)
        {
            return;
        }

        PartyCharacter character =
            save.GetCharacter(
                currentCharacterIndex);

        character.Food =
            (ushort)
                foodNumeric.Value;

        character.Gold =
            (ushort)
                goldNumeric.Value;

        character.Torches =
            (byte)
                torchesNumeric.Value;

        character.MagicGems =
            (byte)
                gemsNumeric.Value;

        character.SkullKeys =
            (byte)
                keysNumeric.Value;

        character.TimeStopPowder =
            (byte)
                powderNumeric.Value;

        character.ForceMark =
            forceMarkCheck.Checked;

        character.FireMark =
            fireMarkCheck.Checked;

        character.SnakeMark =
            snakeMarkCheck.Checked;

        character.KingsMark =
            kingsMarkCheck.Checked;

        character.LoveCard =
            loveCardCheck.Checked;

        character.SolCard =
            solCardCheck.Checked;

        character.MoonCard =
            moonCardCheck.Checked;

        character.DeathCard =
            deathCardCheck.Checked;

        StoreCurrentWeaponQuantity();
        StoreCurrentArmorQuantity();
    }

    public void StoreToSave()
    {
        StoreCurrentCharacter();
    }

    private void CharacterCombo_SelectedIndexChanged(
     object? sender,
     EventArgs e)
    {
        if (loadingControls ||
            save is null)
        {
            return;
        }

        StoreCurrentCharacter();

        ShowCharacterInventory();
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

        StoreCurrentWeaponQuantity();

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

        StoreCurrentArmorQuantity();

        currentArmorIndex =
            armorCombo.SelectedIndex;

        ShowArmorQuantity();
    }
}

