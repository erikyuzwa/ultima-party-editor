using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima4;

public sealed class CharacterPanel
    : UserControl
{
    private readonly ComboBox characterCombo;
    private readonly TextBox nameText;

    private readonly NumericUpDown hpNumeric;
    private readonly NumericUpDown maxHpNumeric;
    private readonly NumericUpDown experienceNumeric;

    private readonly NumericUpDown strengthNumeric;
    private readonly NumericUpDown dexterityNumeric;
    private readonly NumericUpDown intelligenceNumeric;
    private readonly NumericUpDown magicPointsNumeric;

    private readonly ComboBox weaponCombo;
    private readonly ComboBox armorCombo;

    private readonly ComboBox sexCombo;
    private readonly ComboBox classCombo;
    private readonly ComboBox statusCombo;

    private Ultima4SaveFile? save;
    private int currentIndex = -1;

    public CharacterPanel()
    {
        Dock =
            DockStyle.Fill;

        characterCombo =
            new ComboBox
            {
                DropDownStyle =
                    ComboBoxStyle.DropDownList,
                Left = 120,
                Top = 20,
                Width = 220
            };

        nameText =
            new TextBox
            {
                Left = 120,
                Top = 60,
                Width = 220,
                MaxLength = 16
            };

        hpNumeric =
            CreateNumber(120, 100);

        maxHpNumeric =
            CreateNumber(340, 100);

        experienceNumeric =
            CreateNumber(120, 140);

        strengthNumeric =
            CreateNumber(120, 190);

        dexterityNumeric =
            CreateNumber(340, 190);

        intelligenceNumeric =
            CreateNumber(120, 230);

        magicPointsNumeric =
            CreateNumber(340, 230);

        weaponCombo =
            CreateEnumCombo<WeaponType>(
                120,
                280);

        armorCombo =
            CreateEnumCombo<ArmorType>(
                340,
                280);

        sexCombo =
            new ComboBox
            {
                Left = 120,
                Top = 320,
                Width = 150,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        sexCombo.Items.AddRange(
            new object[]
            {
                "Male",
                "Female"
            });

        classCombo =
            new ComboBox
            {
                Left = 120,
                Top = 360,
                Width = 180,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        classCombo.Items.AddRange(
            new object[]
            {
                "Mage",
                "Bard",
                "Fighter",
                "Druid",
                "Tinker",
                "Paladin",
                "Ranger",
                "Shepherd"
            });

        statusCombo =
            new ComboBox
            {
                Left = 120,
                Top = 400,
                Width = 180,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        statusCombo.Items.AddRange(
            new object[]
            {
                "Good",
                "Poisoned",
                "Sleeping",
                "Dead"
            });

        AddLabel("Character:", 20, 23);
        AddLabel("Name:", 20, 63);

        AddLabel("HP:", 20, 103);
        AddLabel("Max HP:", 260, 103);

        AddLabel("Experience:", 20, 143);

        AddLabel("Strength:", 20, 193);
        AddLabel("Dexterity:", 260, 193);

        AddLabel("Intelligence:", 20, 233);
        AddLabel("Magic Points:", 260, 233);

        AddLabel("Weapon:", 20, 283);
        AddLabel("Armor:", 260, 283);

        AddLabel("Sex:", 20, 323);
        AddLabel("Class:", 20, 363);
        AddLabel("Status:", 20, 403);

        Controls.AddRange(
            new Control[]
            {
                characterCombo,
                nameText,

                hpNumeric,
                maxHpNumeric,
                experienceNumeric,

                strengthNumeric,
                dexterityNumeric,
                intelligenceNumeric,
                magicPointsNumeric,

                weaponCombo,
                armorCombo,

                sexCombo,
                classCombo,
                statusCombo
            });

        characterCombo.SelectedIndexChanged +=
            CharacterCombo_SelectedIndexChanged;
    }

    private NumericUpDown CreateNumber(
        int x,
        int y)
    {
        return new NumericUpDown
        {
            Left = x,
            Top = y,
            Width = 100,
            Minimum = 0,
            Maximum = ushort.MaxValue
        };
    }

    private ComboBox CreateEnumCombo<T>(
    int x,
    int y)
    where T : struct, Enum
    {
        ComboBox combo =
            new()
            {
                Left = x,
                Top = y,
                Width = 160,
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
        save =
            saveFile;

        characterCombo.Items.Clear();

        for (int i = 0;
             i < Ultima4SaveFile.CharacterCount;
             i++)
        {
            PartyCharacter character =
                save.GetCharacter(i);

            string name =
                string.IsNullOrWhiteSpace(
                    character.Name)
                    ? "<empty>"
                    : character.Name;

            characterCombo.Items.Add(
                $"{i + 1} - {name}");
        }

        if (characterCombo.Items.Count > 0)
        {
            characterCombo.SelectedIndex =
                0;
        }
    }

    private void CharacterCombo_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (save is null)
            return;

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
            currentIndex);
    }

    private void ShowCharacter(
        int index)
    {
        if (save is null)
            return;

        PartyCharacter character =
            save.GetCharacter(index);

        nameText.Text =
            character.Name;

        hpNumeric.Value =
            character.HitPoints;

        maxHpNumeric.Value =
            character.MaxHitPoints;

        experienceNumeric.Value =
            character.Experience;

        strengthNumeric.Value =
            character.Strength;

        dexterityNumeric.Value =
            character.Dexterity;

        intelligenceNumeric.Value =
            character.Intelligence;

        magicPointsNumeric.Value =
            character.MagicPoints;

        weaponCombo.SelectedIndex =
            (int)character.Weapon;

        armorCombo.SelectedIndex =
            (int)character.Armor;

        sexCombo.SelectedIndex =
            character.Sex == 0x0C
                ? 1
                : 0;

        classCombo.SelectedIndex =
            character.ClassType < 8
                ? character.ClassType
                : -1;

        statusCombo.SelectedIndex =
            character.Status switch
            {
                (byte)'G' => 0,
                (byte)'P' => 1,
                (byte)'S' => 2,
                (byte)'D' => 3,
                _ => -1
            };
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

        character.HitPoints =
            (ushort)hpNumeric.Value;

        character.MaxHitPoints =
            (ushort)maxHpNumeric.Value;

        character.Experience =
            (ushort)experienceNumeric.Value;

        character.Strength =
            (ushort)strengthNumeric.Value;

        character.Dexterity =
            (ushort)dexterityNumeric.Value;

        character.Intelligence =
            (ushort)intelligenceNumeric.Value;

        character.MagicPoints =
            (ushort)magicPointsNumeric.Value;

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

        character.Sex =
            sexCombo.SelectedIndex == 1
                ? (byte)0x0C
                : (byte)0x0B;

        if (classCombo.SelectedIndex >= 0)
        {
            character.ClassType =
                (byte)
                    classCombo.SelectedIndex;
        }

        character.Status =
            statusCombo.SelectedIndex switch
            {
                0 => (byte)'G',
                1 => (byte)'P',
                2 => (byte)'S',
                3 => (byte)'D',
                _ => character.Status
            };
    }
}