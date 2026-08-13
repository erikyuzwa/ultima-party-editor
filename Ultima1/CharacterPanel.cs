using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima1;

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
        hitPointsNumeric;

    private readonly NumericUpDown
        experienceNumeric;

    private readonly NumericUpDown
        strengthNumeric;

    private readonly NumericUpDown
        agilityNumeric;

    private readonly NumericUpDown
        staminaNumeric;

    private readonly NumericUpDown
        charismaNumeric;

    private readonly NumericUpDown
        wisdomNumeric;

    private readonly NumericUpDown
        intelligenceNumeric;

    private Ultima1SaveFile? save;

    public CharacterPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        var characterGroup =
            new GroupBox
            {
                Text = "Character",
                Dock = DockStyle.Top,
                Height = 430
            };

        AddLabel(
            characterGroup,
            "Name:",
            25,
            40);

        nameText =
            new TextBox
            {
                Left = 150,
                Top = 35,
                Width = 220,
                MaxLength = 14
            };

        AddLabel(
            characterGroup,
            "Sex:",
            25,
            80);

        sexCombo =
            CreateEnumCombo<SexType>(
                150,
                75);

        AddLabel(
            characterGroup,
            "Race:",
            25,
            120);

        raceCombo =
            CreateEnumCombo<RaceType>(
                150,
                115);

        AddLabel(
            characterGroup,
            "Class:",
            25,
            160);

        classCombo =
            CreateEnumCombo<ClassType>(
                150,
                155);

        AddLabel(
            characterGroup,
            "Hit:",
            430,
            40);

        hitPointsNumeric =
            CreateNumber(
                560,
                35,
                9999);

        AddLabel(
            characterGroup,
            "Experience:",
            430,
            80);

        experienceNumeric =
            CreateNumber(
                560,
                75,
                9999);

        AddLabel(
            characterGroup,
            "Strength:",
            25,
            230);

        strengthNumeric =
            CreateNumber(
                150,
                225,
                99);

        AddLabel(
            characterGroup,
            "Agility:",
            25,
            270);

        agilityNumeric =
            CreateNumber(
                150,
                265,
                99);

        AddLabel(
            characterGroup,
            "Stamina:",
            25,
            310);

        staminaNumeric =
            CreateNumber(
                150,
                305,
                99);

        AddLabel(
            characterGroup,
            "Charisma:",
            430,
            230);

        charismaNumeric =
            CreateNumber(
                560,
                225,
                99);

        AddLabel(
            characterGroup,
            "Wisdom:",
            430,
            270);

        wisdomNumeric =
            CreateNumber(
                560,
                265,
                99);

        AddLabel(
            characterGroup,
            "Intelligence:",
            430,
            310);

        intelligenceNumeric =
            CreateNumber(
                560,
                305,
                99);

        characterGroup.Controls.AddRange(
            new Control[]
            {
                nameText,

                sexCombo,
                raceCombo,
                classCombo,

                hitPointsNumeric,
                experienceNumeric,

                strengthNumeric,
                agilityNumeric,
                staminaNumeric,

                charismaNumeric,
                wisdomNumeric,
                intelligenceNumeric
            });

        Controls.Add(
            characterGroup);
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

    public void LoadFromSave(
        Ultima1SaveFile saveFile)
    {
        save =
            saveFile;

        PartyCharacter character =
            save.Character;

        nameText.Text =
            character.Name;

        sexCombo.SelectedIndex =
            (int)character.Sex;

        raceCombo.SelectedIndex =
            (int)character.Race;

        classCombo.SelectedIndex =
            (int)character.Class;

        hitPointsNumeric.Value =
            character.HitPoints;

        experienceNumeric.Value =
            character.Experience;

        strengthNumeric.Value =
            character.Strength;

        agilityNumeric.Value =
            character.Agility;

        staminaNumeric.Value =
            character.Stamina;

        charismaNumeric.Value =
            character.Charisma;

        wisdomNumeric.Value =
            character.Wisdom;

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

        if (sexCombo.SelectedIndex >= 0)
        {
            character.Sex =
                (SexType)
                    sexCombo.SelectedIndex;
        }

        if (raceCombo.SelectedIndex >= 0)
        {
            character.Race =
                (RaceType)
                    raceCombo.SelectedIndex;
        }

        if (classCombo.SelectedIndex >= 0)
        {
            character.Class =
                (ClassType)
                    classCombo.SelectedIndex;
        }

        character.HitPoints =
            (ushort)
                hitPointsNumeric.Value;

        character.Experience =
            (ushort)
                experienceNumeric.Value;

        character.Strength =
            (ushort)
                strengthNumeric.Value;

        character.Agility =
            (ushort)
                agilityNumeric.Value;

        character.Stamina =
            (ushort)
                staminaNumeric.Value;

        character.Charisma =
            (ushort)
                charismaNumeric.Value;

        character.Wisdom =
            (ushort)
                wisdomNumeric.Value;

        character.Intelligence =
            (ushort)
                intelligenceNumeric.Value;
    }
}