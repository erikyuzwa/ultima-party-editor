using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima2;

public sealed class CharacterPanel
    : UserControl
{
    private readonly TextBox
        nameText;

    private readonly ComboBox
        sexCombo;

    private readonly ComboBox
        raceCombo;

    private readonly ComboBox
        classCombo;

    private readonly NumericUpDown
        hpNumeric;

    private readonly NumericUpDown
        experienceNumeric;

    private readonly NumericUpDown
        strengthNumeric;

    private readonly NumericUpDown
        staminaNumeric;

    private readonly NumericUpDown
        wisdomNumeric;

    private readonly NumericUpDown
        agilityNumeric;

    private readonly NumericUpDown
        charismaNumeric;

    private readonly NumericUpDown
        intelligenceNumeric;

    private Ultima2SaveFile? save;

    public CharacterPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        var statsGroup =
            new GroupBox
            {
                Text = "Character",
                Dock = DockStyle.Top,
                Height = 430
            };

        AddLabel(
            statsGroup,
            "Name:",
            25,
            40);

        nameText =
            new TextBox
            {
                Left = 150,
                Top = 35,
                Width = 220,
                MaxLength = 12
            };

        AddLabel(
            statsGroup,
            "Sex:",
            25,
            80);

        sexCombo =
            CreateEnumCombo<SexType>(
                150,
                75);

        AddLabel(
            statsGroup,
            "Race:",
            25,
            120);

        raceCombo =
            CreateEnumCombo<RaceType>(
                150,
                115);

        AddLabel(
            statsGroup,
            "Class:",
            25,
            160);

        classCombo =
            CreateEnumCombo<ClassType>(
                150,
                155);

        AddLabel(
            statsGroup,
            "Hit Points:",
            430,
            40);

        hpNumeric =
            CreateNumber(
                560,
                35,
                9999);

        AddLabel(
            statsGroup,
            "Experience:",
            430,
            80);

        experienceNumeric =
            CreateNumber(
                560,
                75,
                9999);

        AddLabel(
            statsGroup,
            "Strength:",
            25,
            230);

        strengthNumeric =
            CreateNumber(
                150,
                225,
                99);

        AddLabel(
            statsGroup,
            "Stamina:",
            25,
            270);

        staminaNumeric =
            CreateNumber(
                150,
                265,
                99);

        AddLabel(
            statsGroup,
            "Wisdom:",
            25,
            310);

        wisdomNumeric =
            CreateNumber(
                150,
                305,
                99);

        AddLabel(
            statsGroup,
            "Agility:",
            430,
            230);

        agilityNumeric =
            CreateNumber(
                560,
                225,
                99);

        AddLabel(
            statsGroup,
            "Charisma:",
            430,
            270);

        charismaNumeric =
            CreateNumber(
                560,
                265,
                99);

        AddLabel(
            statsGroup,
            "Intelligence:",
            430,
            310);

        intelligenceNumeric =
            CreateNumber(
                560,
                305,
                99);

        statsGroup.Controls.AddRange(
            new Control[]
            {
                nameText,

                sexCombo,
                raceCombo,
                classCombo,

                hpNumeric,
                experienceNumeric,

                strengthNumeric,
                staminaNumeric,
                wisdomNumeric,

                agilityNumeric,
                charismaNumeric,
                intelligenceNumeric
            });

        Controls.Add(
            statsGroup);
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

    private static ComboBox CreateEnumCombo<T>(
        int x,
        int y)
        where T : struct, Enum
    {
        ComboBox combo =
            new()
            {
                Left = x,
                Top = y,
                Width = 180,

                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        foreach (T value
                 in Enum.GetValues<T>())
        {
            combo.Items.Add(
                ((Enum)(object)value)
                    .ToDisplayName());
        }

        return combo;
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
       Ultima2SaveFile saveFile)
    {
        save =
            saveFile;

        PartyCharacter character =
            save.Character;

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

        hpNumeric.Value =
            character.HitPoints;

        experienceNumeric.Value =
            character.Experience;

        strengthNumeric.Value =
            character.Strength;

        staminaNumeric.Value =
            character.Stamina;

        wisdomNumeric.Value =
            character.Wisdom;

        agilityNumeric.Value =
            character.Agility;

        charismaNumeric.Value =
            character.Charisma;

        intelligenceNumeric.Value =
            character.Intelligence;
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        PartyCharacter character =
            save.Character;

        character.Name =
            nameText.Text;

        SexType? sex =
            GetEnumSelection<SexType>(
                sexCombo);

        if (sex.HasValue)
        {
            character.Sex =
                sex.Value;
        }

        RaceType? race =
            GetEnumSelection<RaceType>(
                raceCombo);

        if (race.HasValue)
        {
            character.Race =
                race.Value;
        }

        ClassType? characterClass =
            GetEnumSelection<ClassType>(
                classCombo);

        if (characterClass.HasValue)
        {
            character.Class =
                characterClass.Value;
        }

        character.HitPoints =
            (ushort)
                hpNumeric.Value;

        character.Experience =
            (ushort)
                experienceNumeric.Value;

        character.Strength =
            (byte)
                strengthNumeric.Value;

        character.Stamina =
            (byte)
                staminaNumeric.Value;

        character.Wisdom =
            (byte)
                wisdomNumeric.Value;

        character.Agility =
            (byte)
                agilityNumeric.Value;

        character.Charisma =
            (byte)
                charismaNumeric.Value;

        character.Intelligence =
            (byte)
                intelligenceNumeric.Value;
    }
}