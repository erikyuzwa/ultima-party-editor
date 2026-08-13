using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima4;

public sealed class VirtueItemsPanel
    : UserControl
{
    private readonly CheckBox[] stoneChecks =
        new CheckBox[8];

    private readonly NumericUpDown[] virtueValues =
        new NumericUpDown[8];

    private readonly CheckBox[] runeChecks =
        new CheckBox[8];

    private Ultima4SaveFile? save;

    public VirtueItemsPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding = new Padding(20);

        BuildStoneGroup();

        BuildVirtueGroup();
    }

    private void BuildStoneGroup()
    {

        var group =
            new GroupBox
            {
                Text = "Virtue Stones",
                Dock = DockStyle.Top,
                //Left = 20,
                //Top = 20,
                //Width = 760,
                Height = 120
                //Anchor =
                //    AnchorStyles.Top |
                //    AnchorStyles.Left |
                //    AnchorStyles.Right
            };

        StoneType[] stones =
            Enum.GetValues<StoneType>();

        for (int i = 0;
             i < stones.Length;
             i++)
        {
            int column =
                i % 4;

            int row =
                i / 4;

            stoneChecks[i] =
                new CheckBox
                {
                    Text =
                        ((Enum)(object)stones[i])
                            .ToDisplayName(),

                    Left =
                        20 +
                        column * 180,

                    Top =
                        30 +
                        row * 35,

                    Width = 160
                };

            group.Controls.Add(
                stoneChecks[i]);
        }

        Controls.Add(group);
    }

    private void BuildVirtueGroup()
    {
        var group =
            new GroupBox
            {
                Text = "Virtues and Runes",
                Dock = DockStyle.Top,
                //Left = 20,
                //Top = 155,
                //Width = 760,
                Height = 390
                //Anchor =
                //    AnchorStyles.Top |
                //    AnchorStyles.Left |
                //    AnchorStyles.Right
            };

        VirtueType[] virtues =
            Enum.GetValues<VirtueType>();

        for (int i = 0;
             i < virtues.Length;
             i++)
        {
            int y =
                30 +
                i * 42;

            var virtueLabel =
                new Label
                {
                    Text =
                        virtues[i].ToString(),

                    Left = 20,
                    Top = y + 4,
                    Width = 120
                };

            virtueValues[i] =
                new NumericUpDown
                {
                    Left = 150,
                    Top = y,
                    Width = 80,

                    Minimum = 0,
                    Maximum = 100,

                    DecimalPlaces = 0
                };

            var percentLabel =
                new Label
                {
                    Text = "%",
                    Left = 235,
                    Top = y + 4,
                    Width = 25
                };

            runeChecks[i] =
                new CheckBox
                {
                    Text =
                        $"Rune of {virtues[i]}",

                    Left = 285,
                    Top = y + 2,
                    Width = 200
                };

            group.Controls.Add(
                virtueLabel);

            group.Controls.Add(
                virtueValues[i]);

            group.Controls.Add(
                percentLabel);

            group.Controls.Add(
                runeChecks[i]);
        }

        Controls.Add(group);
    }

    public void LoadFromSave(
        Ultima4SaveFile saveFile)
    {
        save =
            saveFile;

        StoneType[] stones =
            Enum.GetValues<StoneType>();

        for (int i = 0;
             i < stones.Length;
             i++)
        {
            stoneChecks[i].Checked =
                save.HasStone(
                    stones[i]);
        }

        VirtueType[] virtues =
            Enum.GetValues<VirtueType>();

        for (int i = 0;
             i < virtues.Length;
             i++)
        {
            ushort rawValue =
                save.GetVirtueValue(
                    virtues[i]);

            virtueValues[i].Value =
                Math.Clamp(
                    rawValue,
                    (ushort)0,
                    (ushort)100);

            runeChecks[i].Checked =
                save.HasRune(
                    virtues[i]);
        }
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        StoneType[] stones =
            Enum.GetValues<StoneType>();

        for (int i = 0;
             i < stones.Length;
             i++)
        {
            save.SetStone(
                stones[i],
                stoneChecks[i].Checked);
        }

        VirtueType[] virtues =
            Enum.GetValues<VirtueType>();

        for (int i = 0;
             i < virtues.Length;
             i++)
        {
            save.SetVirtueValue(
                virtues[i],
                (ushort)
                    virtueValues[i].Value);

            save.SetRune(
                virtues[i],
                runeChecks[i].Checked);
        }
    }
}