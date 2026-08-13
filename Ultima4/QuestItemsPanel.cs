namespace UltimaSaveEditor.Ultima4;

public sealed class QuestItemsPanel
    : UserControl
{
    private readonly CheckBox skullCheck;
    private readonly CheckBox hornCheck;
    private readonly CheckBox wheelCheck;

    private readonly CheckBox candleCheck;
    private readonly CheckBox bookCheck;
    private readonly CheckBox bellCheck;

    private readonly CheckBox keyLoveCheck;
    private readonly CheckBox keyTruthCheck;
    private readonly CheckBox keyCourageCheck;

    private Ultima4SaveFile? save;

    public QuestItemsPanel()
    {
        Dock = DockStyle.Fill;

        var groupBox =
            new GroupBox
            {
                Text = "Quest Items",
                Left = 20,
                Top = 20,
                Width = 620,
                Height = 250,
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

        skullCheck =
            CreateCheckBox(
                "Mondain's Skull",
                25,
                35);

        hornCheck =
            CreateCheckBox(
                "Silver Horn",
                250,
                35);

        wheelCheck =
            CreateCheckBox(
                "Wheel",
                25,
                75);

        candleCheck =
            CreateCheckBox(
                "Candle of Love",
                250,
                75);

        bookCheck =
            CreateCheckBox(
                "Book of Truth",
                25,
                115);

        bellCheck =
            CreateCheckBox(
                "Bell of Courage",
                250,
                115);

        keyLoveCheck =
            CreateCheckBox(
                "Key of Love",
                25,
                165);

        keyTruthCheck =
            CreateCheckBox(
                "Key of Truth",
                250,
                165);

        keyCourageCheck =
            CreateCheckBox(
                "Key of Courage",
                25,
                205);

        groupBox.Controls.AddRange(
            new Control[]
            {
                skullCheck,
                hornCheck,
                wheelCheck,

                candleCheck,
                bookCheck,
                bellCheck,

                keyLoveCheck,
                keyTruthCheck,
                keyCourageCheck
            });

        Controls.Add(groupBox);
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
            Width = 190,
            AutoSize = false
        };
    }

    public void LoadFromSave(
        Ultima4SaveFile saveFile)
    {
        save = saveFile;

        skullCheck.Checked =
            save.HasQuestItem(
                QuestItem.MondainsSkull);

        hornCheck.Checked =
            save.HasQuestItem(
                QuestItem.SilverHorn);

        wheelCheck.Checked =
            save.HasQuestItem(
                QuestItem.Wheel);

        candleCheck.Checked =
            save.HasQuestItem(
                QuestItem.CandleOfLove);

        bookCheck.Checked =
            save.HasQuestItem(
                QuestItem.BookOfTruth);

        bellCheck.Checked =
            save.HasQuestItem(
                QuestItem.BellOfCourage);

        keyLoveCheck.Checked =
            save.HasQuestItem(
                QuestItem.KeyOfLove);

        keyTruthCheck.Checked =
            save.HasQuestItem(
                QuestItem.KeyOfTruth);

        keyCourageCheck.Checked =
            save.HasQuestItem(
                QuestItem.KeyOfCourage);
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        save.SetQuestItem(
            QuestItem.MondainsSkull,
            skullCheck.Checked);

        save.SetQuestItem(
            QuestItem.SilverHorn,
            hornCheck.Checked);

        save.SetQuestItem(
            QuestItem.Wheel,
            wheelCheck.Checked);

        save.SetQuestItem(
            QuestItem.CandleOfLove,
            candleCheck.Checked);

        save.SetQuestItem(
            QuestItem.BookOfTruth,
            bookCheck.Checked);

        save.SetQuestItem(
            QuestItem.BellOfCourage,
            bellCheck.Checked);

        save.SetQuestItem(
            QuestItem.KeyOfLove,
            keyLoveCheck.Checked);

        save.SetQuestItem(
            QuestItem.KeyOfTruth,
            keyTruthCheck.Checked);

        save.SetQuestItem(
            QuestItem.KeyOfCourage,
            keyCourageCheck.Checked);
    }
}