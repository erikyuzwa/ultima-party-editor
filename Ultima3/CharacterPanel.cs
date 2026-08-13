using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima3;

public sealed class CharacterPanel
    : UserControl
{

    private readonly TextBox nameText;

    private readonly ComboBox sexCombo;
    private readonly ComboBox classCombo;
    private readonly ComboBox raceCombo;
    private readonly ComboBox healthCombo;

    private readonly ComboBox weaponCombo;
    private readonly ComboBox armorCombo;

    private readonly NumericUpDown hitPointsNumeric;
    private readonly NumericUpDown maxHitPointsNumeric;
    private readonly NumericUpDown experienceNumeric;

    private readonly NumericUpDown strengthNumeric;
    private readonly NumericUpDown dexterityNumeric;
    private readonly NumericUpDown intelligenceNumeric;
    private readonly NumericUpDown wisdomNumeric;
    private readonly NumericUpDown magicPointsNumeric;

    private Ultima3SaveFile? save;

    private int currentIndex = -1;
    private bool loadingControls;

    public CharacterPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        nameText =
            new TextBox
            {
                Left = 140,
                Top = 65,
                Width = 220,
                MaxLength = 9
            };

        sexCombo =
            CreateEnumCombo<SexType>(
                140,
                105);

        raceCombo =
            CreateEnumCombo<RaceType>(
                140,
                145);

        classCombo =
            CreateEnumCombo<ClassType>(
                140,
                185);

        healthCombo =
            CreateEnumCombo<HealthStatus>(
                140,
                225);

        weaponCombo =
            CreateEnumCombo<WeaponType>(
                140,
                265,
                190);

        armorCombo =
            CreateEnumCombo<ArmorType>(
                140,
                305,
                190);

        hitPointsNumeric =
            CreateNumber(
                540,
                25,
                9999);

        maxHitPointsNumeric =
            CreateNumber(
                540,
                65,
                9999);

        experienceNumeric =
            CreateNumber(
                540,
                105,
                9999);

        strengthNumeric =
            CreateNumber(
                540,
                165,
                99);

        dexterityNumeric =
            CreateNumber(
                540,
                205,
                99);

        intelligenceNumeric =
            CreateNumber(
                540,
                245,
                99);

        wisdomNumeric =
            CreateNumber(
                540,
                285,
                99);

        magicPointsNumeric =
            CreateNumber(
                540,
                325,
                99);

        AddLabel(
            "Character:",
            25,
            30);

        AddLabel(
            "Name:",
            25,
            70);

        AddLabel(
            "Sex:",
            25,
            110);

        AddLabel(
            "Race:",
            25,
            150);

        AddLabel(
            "Class:",
            25,
            190);

        AddLabel(
            "Health:",
            25,
            230);

        AddLabel(
            "Weapon:",
            25,
            270);

        AddLabel(
            "Armor:",
            25,
            310);

        AddLabel(
            "Hit Points:",
            410,
            30);

        AddLabel(
            "Max Hit Points:",
            410,
            70);

        AddLabel(
            "Experience:",
            410,
            110);

        AddLabel(
            "Strength:",
            410,
            170);

        AddLabel(
            "Dexterity:",
            410,
            210);

        AddLabel(
            "Intelligence:",
            410,
            250);

        AddLabel(
            "Wisdom:",
            410,
            290);

        AddLabel(
            "Magic Points:",
            410,
            330);

        Controls.AddRange(
            new Control[]
            {
                //characterCombo,
                nameText,

                sexCombo,
                raceCombo,
                classCombo,
                healthCombo,

                weaponCombo,
                armorCombo,

                hitPointsNumeric,
                maxHitPointsNumeric,
                experienceNumeric,

                strengthNumeric,
                dexterityNumeric,
                intelligenceNumeric,
                wisdomNumeric,
                magicPointsNumeric
            });

        //characterCombo.SelectedIndexChanged +=
        //    CharacterCombo_SelectedIndexChanged;
    }

    private ComboBox CreateCombo(
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

    private ComboBox CreateEnumCombo<T>(
        int x,
        int y,
        int width = 170)
        where T : struct, Enum
    {
        ComboBox combo =
            CreateCombo(
                x,
                y,
                width);

        foreach (T value
                 in Enum.GetValues<T>())
        {
            combo.Items.Add(
                ((Enum)(object)value)
                    .ToDisplayName());
        }

        return combo;
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
            Width = 110,
            Minimum = 0,
            Maximum = maximum
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

        if (currentIndex >= 0 &&
            currentIndex != index)
        {
            StoreCurrentCharacter();
        }

        currentIndex =
            index;

        ShowCharacter(
            index);
    }

    public void LoadFromSave(
        Ultima3SaveFile saveFile)
    {
        save =
            saveFile;

       
    }

    private void ShowCharacter(
        int index)
    {
        if (save is null)
            return;

        PartyCharacter character =
            save.GetCharacter(index);

        loadingControls = true;

        nameText.Text =
            character.Name;

        SetEnumSelection(
            sexCombo,
            character.Sex);

        SetEnumSelection(
            raceCombo,
            character.Race);

        SetEnumSelection(
            classCombo,
            character.Class);

        SetEnumSelection(
            healthCombo,
            character.Health);

        weaponCombo.SelectedIndex =
            (int)character.Weapon;

        armorCombo.SelectedIndex =
            (int)character.Armor;

        hitPointsNumeric.Value =
            character.HitPoints;

        maxHitPointsNumeric.Value =
            character.MaxHitPoints;

        experienceNumeric.Value =
            character.Experience;

        strengthNumeric.Value =
            character.Strength;

        dexterityNumeric.Value =
            character.Dexterity;

        intelligenceNumeric.Value =
            character.Intelligence;

        wisdomNumeric.Value =
            character.Wisdom;

        magicPointsNumeric.Value =
            character.MagicPoints;

        loadingControls = false;
    }

    private static void SetEnumSelection<T>(
       ComboBox combo,
       T value)
       where T : struct, Enum
    {
        T[] values =
            Enum.GetValues<T>();

        int index =
            Array.IndexOf(
                values,
                value);

        combo.SelectedIndex =
            index;
    }

    private static T? GetEnumSelection<T>(
        ComboBox combo)
        where T : struct, Enum
    {
        int index =
            combo.SelectedIndex;

        T[] values =
            Enum.GetValues<T>();

        if (index < 0 ||
            index >= values.Length)
        {
            return null;
        }

        return values[index];
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

        RaceType? race =
            GetEnumSelection<RaceType>(
                raceCombo);

        if (race.HasValue)
            character.Race = race.Value;

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

        if (weaponCombo.SelectedIndex >= 0)
        {
            character.Weapon =
                (WeaponType)
                    weaponCombo.SelectedIndex;
        }

        if (armorCombo.SelectedIndex >= 0)
        {
            character.Armor =
                (ArmorType)
                    armorCombo.SelectedIndex;
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

        character.Strength =
            (byte)
                strengthNumeric.Value;

        character.Dexterity =
            (byte)
                dexterityNumeric.Value;

        character.Intelligence =
            (byte)
                intelligenceNumeric.Value;

        character.Wisdom =
            (byte)
                wisdomNumeric.Value;

        character.MagicPoints =
            (byte)
                magicPointsNumeric.Value;

        //
        // Update the selector if the name changed.
        //
        //characterCombo.Items[
        //    currentIndex] =
        //    string.IsNullOrWhiteSpace(
        //        character.Name)
        //        ? $"Character {currentIndex + 1}"
        //        : character.Name;
    }
}