using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima5;

public sealed class CharacterPanel
    : UserControl
{
    private readonly ComboBox characterCombo;

    private readonly TextBox nameText;

    private readonly ComboBox sexCombo;
    private readonly ComboBox classCombo;
    private readonly ComboBox healthCombo;

    private readonly NumericUpDown hitPointsNumeric;
    private readonly NumericUpDown maxHitPointsNumeric;
    private readonly NumericUpDown experienceNumeric;
    private readonly NumericUpDown levelNumeric;

    private readonly NumericUpDown strengthNumeric;
    private readonly NumericUpDown dexterityNumeric;
    private readonly NumericUpDown intelligenceNumeric;
    private readonly NumericUpDown magicNumeric;

    private readonly ComboBox weaponCombo;
    private readonly ComboBox armorCombo;
    private readonly ComboBox helmCombo;
    private readonly ComboBox shieldCombo;
    private readonly ComboBox amuletCombo;
    private readonly ComboBox ringCombo;

    private Ultima5SaveFile? save;

    private int currentIndex = -1;

    private bool loadingControls;

    public CharacterPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        characterCombo =
            CreateCombo(
                150,
                20,
                240);

        AddLabel(
            this,
            "Character:",
            25,
            25);

        nameText =
            new TextBox
            {
                Left = 150,
                Top = 65,
                Width = 220,
                MaxLength = 8
            };

        AddLabel(
            this,
            "Name:",
            25,
            70);

        sexCombo =
            CreateEnumCombo<SexType>(
                150,
                105);

        AddLabel(
            this,
            "Sex:",
            25,
            110);

        classCombo =
            CreateEnumCombo<ClassType>(
                150,
                145);

        AddLabel(
            this,
            "Class:",
            25,
            150);

        healthCombo =
            CreateEnumCombo<HealthStatus>(
                150,
                185);

        AddLabel(
            this,
            "Health:",
            25,
            190);

        hitPointsNumeric =
            CreateNumber(
                520,
                20,
                9999);

        AddLabel(
            this,
            "Hit Points:",
            400,
            25);

        maxHitPointsNumeric =
            CreateNumber(
                520,
                60,
                9999);

        AddLabel(
            this,
            "Max Hit Points:",
            400,
            65);

        experienceNumeric =
            CreateNumber(
                520,
                100,
                9999);

        AddLabel(
            this,
            "Experience:",
            400,
            105);

        levelNumeric =
            CreateNumber(
                520,
                140,
                255);

        AddLabel(
            this,
            "Level:",
            400,
            145);

        strengthNumeric =
            CreateNumber(
                150,
                255,
                99);

        AddLabel(
            this,
            "Strength:",
            25,
            260);

        dexterityNumeric =
            CreateNumber(
                150,
                295,
                99);

        AddLabel(
            this,
            "Dexterity:",
            25,
            300);

        intelligenceNumeric =
            CreateNumber(
                520,
                255,
                99);

        AddLabel(
            this,
            "Intelligence:",
            400,
            260);

        magicNumeric =
            CreateNumber(
                520,
                295,
                255);

        AddLabel(
            this,
            "Magic:",
            400,
            300);

        helmCombo =
                    CreateEquipmentCombo(
                        150,
                        370);

        shieldCombo =
            CreateEquipmentCombo(
                150,
                410);

        armorCombo =
            CreateEquipmentCombo(
                150,
                450);

        weaponCombo =
            CreateEquipmentCombo(
                520,
                370);

        ringCombo =
            CreateEquipmentCombo(
                520,
                410);

        amuletCombo =
            CreateEquipmentCombo(
                520,
                450);

        AddLabel(
            this,
            "Helm:",
            25,
            375);

        AddLabel(
            this,
            "Shield:",
            25,
            415);

        AddLabel(
            this,
            "Armor:",
            25,
            455);

        AddLabel(
            this,
            "Weapon:",
            400,
            375);

        AddLabel(
            this,
            "Ring:",
            400,
            415);

        AddLabel(
            this,
            "Amulet:",
            400,
            455);

        Controls.AddRange(
            new Control[]
            {
                characterCombo,
                nameText,

                sexCombo,
                classCombo,
                healthCombo,

                hitPointsNumeric,
                maxHitPointsNumeric,
                experienceNumeric,
                levelNumeric,

                strengthNumeric,
                dexterityNumeric,
                intelligenceNumeric,
                magicNumeric,

                weaponCombo,
                armorCombo,
                helmCombo,
                shieldCombo,
                amuletCombo,
                ringCombo
            });

        characterCombo.SelectedIndexChanged +=
            CharacterCombo_SelectedIndexChanged;
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
            Maximum = maximum
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

    private static ComboBox CreateEnumCombo<T>(
        int x,
        int y)
        where T : struct, Enum
    {
        ComboBox combo =
            CreateCombo(
                x,
                y,
                190);

        foreach (T value
                 in Enum.GetValues<T>())
        {
            combo.Items.Add(
                ((Enum)(object)value)
                    .ToDisplayName());
        }

        return combo;
    }

    private static ComboBox CreateEquipmentCombo(
        int x,
        int y)
    {
        ComboBox combo =
            CreateCombo(
                x,
                y,
                210);

        foreach (EquipmentItemType value
                 in Enum.GetValues<EquipmentItemType>())
        {
            combo.Items.Add(
                value.ToDisplayName());
        }

        return combo;
    }

    private static void SetEnumSelection<T>(
        ComboBox combo,
        T value)
        where T : struct, Enum
    {
        T[] values =
            Enum.GetValues<T>();

        combo.SelectedIndex =
            Array.IndexOf(
                values,
                value);
    }

    private static T? GetEnumSelection<T>(
        ComboBox combo)
        where T : struct, Enum
    {
        T[] values =
            Enum.GetValues<T>();

        int index =
            combo.SelectedIndex;

        if (index < 0 ||
            index >= values.Length)
        {
            return null;
        }

        return values[index];
    }

    public void LoadFromSave(
       Ultima5SaveFile saveFile)
    {
        save =
            saveFile;

        loadingControls =
            true;

        characterCombo.Items.Clear();

        for (int i = 0;
             i < Ultima5SaveFile.CharacterCount;
             i++)
        {
            PartyCharacter character =
                save.GetCharacter(i);

            characterCombo.Items.Add(
                string.IsNullOrWhiteSpace(
                    character.Name)
                    ? $"Character {i + 1}"
                    : character.Name);
        }

        currentIndex =
            0;

        characterCombo.SelectedIndex =
            0;

        ShowCharacter(0);

        loadingControls =
            false;
    }

    private void ShowCharacter(
        int index)
    {
        if (save is null)
            return;

        loadingControls =
            true;

        PartyCharacter character =
            save.GetCharacter(index);

        nameText.Text =
            character.Name;

        SetEnumSelection(
            sexCombo,
            character.Sex);

        SetEnumSelection(
            classCombo,
            character.Class);

        SetEnumSelection(
            healthCombo,
            character.Health);

        hitPointsNumeric.Value =
            character.HitPoints;

        maxHitPointsNumeric.Value =
            character.MaxHitPoints;

        experienceNumeric.Value =
            character.Experience;

        levelNumeric.Value =
            character.Level;

        strengthNumeric.Value =
            character.Strength;

        dexterityNumeric.Value =
            character.Dexterity;

        intelligenceNumeric.Value =
            character.Intelligence;

        magicNumeric.Value =
            character.MagicPoints;

        SetEnumSelection(
            helmCombo,
            character.Helm);

        SetEnumSelection(
            shieldCombo,
            character.Shield);

        SetEnumSelection(
            armorCombo,
            character.Armor);

        SetEnumSelection(
            weaponCombo,
            character.Weapon);

        SetEnumSelection(
            ringCombo,
            character.Ring);

        SetEnumSelection(
            amuletCombo,
            character.Amulet);

        loadingControls =
            false;
    }

    public void StoreCurrentCharacter()
    {
        if (save is null ||
            currentIndex < 0)
        {
            return;
        }

        PartyCharacter character =
            save.GetCharacter(
                currentIndex);

        character.Name =
            nameText.Text;

        SexType? sex =
            GetEnumSelection<SexType>(
                sexCombo);

        if (sex.HasValue)
            character.Sex = sex.Value;

        ClassType? characterClass =
            GetEnumSelection<ClassType>(
                classCombo);

        if (characterClass.HasValue)
        {
            character.Class =
                characterClass.Value;
        }

        HealthStatus? health =
            GetEnumSelection<HealthStatus>(
                healthCombo);

        if (health.HasValue)
        {
            character.Health =
                health.Value;
        }

        character.HitPoints =
            (ushort)
                hitPointsNumeric.Value;

        character.MaxHitPoints =
            (ushort)
                maxHitPointsNumeric.Value;

        character.Experience =
            (ushort)
                experienceNumeric.Value;

        character.Level =
            (byte)
                levelNumeric.Value;

        character.Strength =
            (byte)
                strengthNumeric.Value;

        character.Dexterity =
            (byte)
                dexterityNumeric.Value;

        character.Intelligence =
            (byte)
                intelligenceNumeric.Value;

        character.MagicPoints =
            (byte)
                magicNumeric.Value;

        EquipmentItemType? helm =
            GetEnumSelection<EquipmentItemType>(
                helmCombo);

        if (helm.HasValue)
            character.Helm = helm.Value;

        EquipmentItemType? shield =
            GetEnumSelection<EquipmentItemType>(
                shieldCombo);

        if (shield.HasValue)
            character.Shield = shield.Value;

        EquipmentItemType? armor =
            GetEnumSelection<EquipmentItemType>(
                armorCombo);

        if (armor.HasValue)
            character.Armor = armor.Value;

        EquipmentItemType? weapon =
            GetEnumSelection<EquipmentItemType>(
                weaponCombo);

        if (weapon.HasValue)
            character.Weapon = weapon.Value;

        EquipmentItemType? ring =
            GetEnumSelection<EquipmentItemType>(
                ringCombo);

        if (ring.HasValue)
            character.Ring = ring.Value;

        EquipmentItemType? amulet =
            GetEnumSelection<EquipmentItemType>(
                amuletCombo);

        if (amulet.HasValue)
            character.Amulet = amulet.Value;
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

        int newIndex =
            characterCombo.SelectedIndex;

        if (newIndex < 0)
            return;

        if (currentIndex >= 0)
        {
            StoreCurrentCharacter();
        }

        currentIndex =
            newIndex;

        ShowCharacter(
            newIndex);
    }
}

