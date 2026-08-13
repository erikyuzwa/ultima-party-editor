namespace UltimaSaveEditor.Ultima2;

public sealed class ItemsPanel
    : UserControl
{
    private NumericUpDown bootsNumeric = null!;
    private NumericUpDown cloaksNumeric = null!;
    private NumericUpDown helmsNumeric = null!;

    private NumericUpDown ringsNumeric = null!;
    private NumericUpDown wandsNumeric = null!;
    private NumericUpDown staffsNumeric = null!;

    private NumericUpDown ankhsNumeric = null!;
    private NumericUpDown brassButtonsNumeric = null!;
    private NumericUpDown blueTasslesNumeric = null!;

    private NumericUpDown greenIdolsNumeric = null!;
    private NumericUpDown triLithiumsNumeric = null!;

    private Ultima2SaveFile? save;

    public ItemsPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        BuildLayout();
    }

    private void BuildLayout()
    {
        var itemsGroup =
            new GroupBox
            {
                Text = "Items",
                Dock = DockStyle.Top,
                Height = 360
            };

        int leftLabel = 25;
        int leftControl = 165;

        int rightLabel = 390;
        int rightControl = 540;

        int y = 40;

        AddLabel(
            itemsGroup,
            "Boots:",
            leftLabel,
            y + 5);

        bootsNumeric =
            CreateNumber(
                leftControl,
                y);

        AddLabel(
            itemsGroup,
            "Rings:",
            rightLabel,
            y + 5);

        ringsNumeric =
            CreateNumber(
                rightControl,
                y);

        y += 45;

        AddLabel(
            itemsGroup,
            "Cloaks:",
            leftLabel,
            y + 5);

        cloaksNumeric =
            CreateNumber(
                leftControl,
                y);

        AddLabel(
            itemsGroup,
            "Wands:",
            rightLabel,
            y + 5);

        wandsNumeric =
            CreateNumber(
                rightControl,
                y);

        y += 45;

        AddLabel(
            itemsGroup,
            "Helms:",
            leftLabel,
            y + 5);

        helmsNumeric =
            CreateNumber(
                leftControl,
                y);

        AddLabel(
            itemsGroup,
            "Staff:",
            rightLabel,
            y + 5);

        staffsNumeric =
            CreateNumber(
                rightControl,
                y);

        y += 45;

        AddLabel(
            itemsGroup,
            "Ankhs:",
            leftLabel,
            y + 5);

        ankhsNumeric =
            CreateNumber(
                leftControl,
                y);

        AddLabel(
            itemsGroup,
            "Brass Buttons:",
            rightLabel,
            y + 5);

        brassButtonsNumeric =
            CreateNumber(
                rightControl,
                y);

        y += 45;

        AddLabel(
            itemsGroup,
            "Blue Tassles:",
            leftLabel,
            y + 5);

        blueTasslesNumeric =
            CreateNumber(
                leftControl,
                y);

        AddLabel(
            itemsGroup,
            "Green Idols:",
            rightLabel,
            y + 5);

        greenIdolsNumeric =
            CreateNumber(
                rightControl,
                y);

        y += 45;

        AddLabel(
            itemsGroup,
            "Tri-Lithiums:",
            leftLabel,
            y + 5);

        triLithiumsNumeric =
            CreateNumber(
                leftControl,
                y);

        itemsGroup.Controls.AddRange(
            new Control[]
            {
                bootsNumeric,
                cloaksNumeric,
                helmsNumeric,

                ringsNumeric,
                wandsNumeric,
                staffsNumeric,

                ankhsNumeric,
                brassButtonsNumeric,
                blueTasslesNumeric,

                greenIdolsNumeric,
                triLithiumsNumeric
            });

        Controls.Add(
            itemsGroup);
    }

    private static NumericUpDown CreateNumber(
        int x,
        int y)
    {
        return new NumericUpDown
        {
            Left = x,
            Top = y,
            Width = 110,

            Minimum = 0,
            Maximum = 99
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

        bootsNumeric.Value =
            save.Boots;

        cloaksNumeric.Value =
            save.Cloaks;

        helmsNumeric.Value =
            save.Helms;

        ringsNumeric.Value =
            save.Rings;

        wandsNumeric.Value =
            save.Wands;

        staffsNumeric.Value =
            save.Staffs;

        ankhsNumeric.Value =
            save.Ankhs;

        brassButtonsNumeric.Value =
            save.BrassButtons;

        blueTasslesNumeric.Value =
            save.BlueTassles;

        greenIdolsNumeric.Value =
            save.GreenIdols;

        triLithiumsNumeric.Value =
            save.TriLithiums;
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        save.Boots =
            (byte)
                bootsNumeric.Value;

        save.Cloaks =
            (byte)
                cloaksNumeric.Value;

        save.Helms =
            (byte)
                helmsNumeric.Value;

        save.Rings =
            (byte)
                ringsNumeric.Value;

        save.Wands =
            (byte)
                wandsNumeric.Value;

        save.Staffs =
            (byte)
                staffsNumeric.Value;

        save.Ankhs =
            (byte)
                ankhsNumeric.Value;

        save.BrassButtons =
            (byte)
                brassButtonsNumeric.Value;

        save.BlueTassles =
            (byte)
                blueTasslesNumeric.Value;

        save.GreenIdols =
            (byte)
                greenIdolsNumeric.Value;

        save.TriLithiums =
            (byte)
                triLithiumsNumeric.Value;
    }
}