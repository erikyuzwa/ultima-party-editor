namespace UltimaSaveEditor.Ultima5;

public sealed class QuestItemsPanel
    : UserControl
{
    private readonly CheckBox grappleCheck;
    private readonly CheckBox amuletCheck;
    private readonly CheckBox crownCheck;
    private readonly CheckBox magicCarpetCheck;

    private readonly CheckBox pocketWatchCheck;
    private readonly CheckBox sceptreCheck;

    private readonly CheckBox shardHatredCheck;
    private readonly CheckBox shardCowardiceCheck;
    private readonly CheckBox shardFalsehoodCheck;

    private readonly CheckBox hmsCapePlansCheck;

    private readonly CheckBox sextantCheck;
    private readonly CheckBox spyGlassCheck;

    private readonly CheckBox blackBadgeCheck;
    private readonly CheckBox sandalwoodBoxCheck;

    private Ultima5SaveFile? save;

    public QuestItemsPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        var artifactsGroup =
            new GroupBox
            {
                Text = "Lord British's Artifacts",
                Dock = DockStyle.Top,
                Height = 120
            };

        amuletCheck =
            CreateCheckBox(
                "Amulet",
                25,
                35);

        crownCheck =
            CreateCheckBox(
                "Crown",
                225,
                35);

        sceptreCheck =
            CreateCheckBox(
                "Sceptre",
                425,
                35);

        sandalwoodBoxCheck =
            CreateCheckBox(
                "Sandalwood Box",
                625,
                35);

        artifactsGroup.Controls.AddRange(
            new Control[]
            {
                amuletCheck,
                crownCheck,
                sceptreCheck,
                sandalwoodBoxCheck
            });

        var shardsGroup =
            new GroupBox
            {
                Text = "Shadowlord Shards",
                Dock = DockStyle.Top,
                Height = 120
            };

        shardFalsehoodCheck =
            CreateCheckBox(
                "Shard of Falsehood",
                25,
                35);

        shardHatredCheck =
            CreateCheckBox(
                "Shard of Hatred",
                275,
                35);

        shardCowardiceCheck =
            CreateCheckBox(
                "Shard of Cowardice",
                525,
                35);

        shardsGroup.Controls.AddRange(
            new Control[]
            {
                shardFalsehoodCheck,
                shardHatredCheck,
                shardCowardiceCheck
            });

        var utilityGroup =
            new GroupBox
            {
                Text = "Quest & Utility Items",
                Dock = DockStyle.Top,
                Height = 220
            };

        grappleCheck =
            CreateCheckBox(
                "Grapple",
                25,
                35);

        magicCarpetCheck =
            CreateCheckBox(
                "Magic Carpet",
                225,
                35);

        pocketWatchCheck =
            CreateCheckBox(
                "Pocket Watch",
                425,
                35);

        hmsCapePlansCheck =
            CreateCheckBox(
                "HMS Cape Plans",
                625,
                35);

        sextantCheck =
            CreateCheckBox(
                "Sextant",
                25,
                80);

        spyGlassCheck =
            CreateCheckBox(
                "Spy Glass",
                225,
                80);

        blackBadgeCheck =
            CreateCheckBox(
                "Black Badge",
                425,
                80);

        utilityGroup.Controls.AddRange(
            new Control[]
            {
                grappleCheck,
                magicCarpetCheck,
                pocketWatchCheck,
                hmsCapePlansCheck,

                sextantCheck,
                spyGlassCheck,
                blackBadgeCheck
            });

        //
        // Add bottom-most group first because all
        // three are DockStyle.Top.
        //
        Controls.Add(
            utilityGroup);

        Controls.Add(
            shardsGroup);

        Controls.Add(
            artifactsGroup);
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

    public void LoadFromSave(
        Ultima5SaveFile saveFile)
    {
        save =
            saveFile;

        grappleCheck.Checked =
            save.HasQuestItem(
                QuestItemType.Grapple);

        amuletCheck.Checked =
            save.HasQuestItem(
                QuestItemType.Amulet);

        crownCheck.Checked =
            save.HasQuestItem(
                QuestItemType.Crown);

        magicCarpetCheck.Checked =
            save.HasQuestItem(
                QuestItemType.MagicCarpet);

        pocketWatchCheck.Checked =
            save.HasQuestItem(
                QuestItemType.PocketWatch);

        sceptreCheck.Checked =
            save.HasQuestItem(
                QuestItemType.Sceptre);

        shardHatredCheck.Checked =
            save.HasQuestItem(
                QuestItemType.ShardOfHatred);

        shardCowardiceCheck.Checked =
            save.HasQuestItem(
                QuestItemType.ShardOfCowardice);

        shardFalsehoodCheck.Checked =
            save.HasQuestItem(
                QuestItemType.ShardOfFalsehood);

        hmsCapePlansCheck.Checked =
            save.HasQuestItem(
                QuestItemType.HmsCapePlans);

        sextantCheck.Checked =
            save.HasQuestItem(
                QuestItemType.Sextant);

        spyGlassCheck.Checked =
            save.HasQuestItem(
                QuestItemType.SpyGlass);

        blackBadgeCheck.Checked =
            save.HasQuestItem(
                QuestItemType.BlackBadge);

        sandalwoodBoxCheck.Checked =
            save.HasQuestItem(
                QuestItemType.SandalwoodBox);
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        save.SetQuestItem(
            QuestItemType.Grapple,
            grappleCheck.Checked);

        save.SetQuestItem(
            QuestItemType.Amulet,
            amuletCheck.Checked);

        save.SetQuestItem(
            QuestItemType.Crown,
            crownCheck.Checked);

        save.SetQuestItem(
            QuestItemType.MagicCarpet,
            magicCarpetCheck.Checked);

        save.SetQuestItem(
            QuestItemType.PocketWatch,
            pocketWatchCheck.Checked);

        save.SetQuestItem(
            QuestItemType.Sceptre,
            sceptreCheck.Checked);

        save.SetQuestItem(
            QuestItemType.ShardOfHatred,
            shardHatredCheck.Checked);

        save.SetQuestItem(
            QuestItemType.ShardOfCowardice,
            shardCowardiceCheck.Checked);

        save.SetQuestItem(
            QuestItemType.ShardOfFalsehood,
            shardFalsehoodCheck.Checked);

        save.SetQuestItem(
            QuestItemType.HmsCapePlans,
            hmsCapePlansCheck.Checked);

        save.SetQuestItem(
            QuestItemType.Sextant,
            sextantCheck.Checked);

        save.SetQuestItem(
            QuestItemType.SpyGlass,
            spyGlassCheck.Checked);

        save.SetQuestItem(
            QuestItemType.BlackBadge,
            blackBadgeCheck.Checked);

        save.SetQuestItem(
            QuestItemType.SandalwoodBox,
            sandalwoodBoxCheck.Checked);
    }
}
