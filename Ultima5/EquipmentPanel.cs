using UltimaSaveEditor.Common;

namespace UltimaSaveEditor.Ultima5;

public sealed class EquipmentPanel
    : UserControl
{
    private readonly EnumQuantitySelector<SpellType>
        spellSelector;

    private readonly EnumQuantitySelector<ReagentType>
        reagentSelector;

    private readonly EnumQuantitySelector<ScrollType>
        scrollSelector;

    private readonly EnumQuantitySelector<PotionType>
        potionSelector;

    private readonly EnumQuantitySelector<WeaponType>
        weaponSelector;

    private readonly EnumQuantitySelector<ArmorType>
        armorSelector;

    private readonly EnumQuantitySelector<HelmType>
        helmSelector;

    private readonly EnumQuantitySelector<ShieldType>
        shieldSelector;

    private readonly EnumQuantitySelector<RingType>
        ringSelector;

    private readonly EnumQuantitySelector<AmuletType>
        amuletSelector;

    private SpellType currentSpell;
    private ReagentType currentReagent;
    private ScrollType currentScroll;
    private PotionType currentPotion;

    private WeaponType currentWeapon;
    private ArmorType currentArmor;
    private HelmType currentHelm;
    private ShieldType currentShield;

    private RingType currentRing;
    private AmuletType currentAmulet;

    private NumericUpDown foodNumeric = null!;
    private NumericUpDown goldNumeric = null!;

    private NumericUpDown torchesNumeric = null!;
    private NumericUpDown keysNumeric = null!;

    private NumericUpDown skullKeysNumeric = null!;
    private NumericUpDown gemsNumeric = null!;

    private Ultima5SaveFile? save;

    private bool loading;

    public EquipmentPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(10);

        spellSelector =
            new EnumQuantitySelector<SpellType>(
                "Spell");

        reagentSelector =
            new EnumQuantitySelector<ReagentType>(
                "Reagent");

        scrollSelector =
            new EnumQuantitySelector<ScrollType>(
                "Scroll");

        potionSelector =
            new EnumQuantitySelector<PotionType>(
                "Potion");

        weaponSelector =
            new EnumQuantitySelector<WeaponType>(
                "Weapon");

        armorSelector =
            new EnumQuantitySelector<ArmorType>(
                "Armor");

        helmSelector =
            new EnumQuantitySelector<HelmType>(
                "Helm");

        shieldSelector =
            new EnumQuantitySelector<ShieldType>(
                "Shield");

        ringSelector =
            new EnumQuantitySelector<RingType>(
                "Ring");

        amuletSelector =
            new EnumQuantitySelector<AmuletType>(
                "Amulet");

        BuildLayout();

        WireEvents();
    }

    private void BuildLayout()
    {
        var layout =
        new TableLayoutPanel
        {
            Dock =
                DockStyle.Top,

            AutoSize =
                true,

            ColumnCount =
                2,

            RowCount =
                6,

            GrowStyle =
                TableLayoutPanelGrowStyle.FixedSize
        };

        layout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        layout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        //
        // Five equipment rows.
        //
        for (int i = 0;
             i < 5;
             i++)
        {
            layout.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    85));
        }

        //
        // Utility row.
        //
        layout.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                180));

        AddGroup(
            layout,
            spellSelector,
            "Spells",
            0,
            0);

        AddGroup(
            layout,
            reagentSelector,
            "Reagents",
            1,
            0);

        AddGroup(
            layout,
            scrollSelector,
            "Scrolls",
            0,
            1);

        AddGroup(
            layout,
            potionSelector,
            "Potions",
            1,
            1);

        AddGroup(
            layout,
            weaponSelector,
            "Weapons",
            0,
            2);

        AddGroup(
            layout,
            armorSelector,
            "Armor",
            1,
            2);

        AddGroup(
            layout,
            helmSelector,
            "Helms",
            0,
            3);

        AddGroup(
            layout,
            shieldSelector,
            "Shields",
            1,
            3);

        AddGroup(
            layout,
            ringSelector,
            "Rings",
            0,
            4);

        AddGroup(
            layout,
            amuletSelector,
            "Amulets",
            1,
            4);

        var utilityGroup =
            new GroupBox
            {
                Text =
                    "Utility",

                Dock =
                    DockStyle.Fill,

                Margin =
                    new Padding(8)
            };

        BuildUtilityGroup(
            utilityGroup);

        layout.SetColumnSpan(
            utilityGroup,
            2);

        layout.Controls.Add(
            utilityGroup,
            0,
            5);

        Controls.Add(
            layout);
    }

    private void BuildUtilityGroup(
    GroupBox group)
    {
        int leftLabel =
            25;

        int leftControl =
            140;

        int rightLabel =
            390;

        int rightControl =
            505;

        int y =
            35;

        AddLabel(
            group,
            "Food:",
            leftLabel,
            y + 5);

        foodNumeric =
            CreateNumber(
                leftControl,
                y,
                9999);

        AddLabel(
            group,
            "Gold:",
            rightLabel,
            y + 5);

        goldNumeric =
            CreateNumber(
                rightControl,
                y,
                9999);

        y += 30;

        AddLabel(
            group,
            "Torches:",
            leftLabel,
            y + 5);

        torchesNumeric =
            CreateNumber(
                leftControl,
                y,
                99);

        AddLabel(
            group,
            "Keys:",
            rightLabel,
            y + 5);

        keysNumeric =
            CreateNumber(
                rightControl,
                y,
                99);

        y += 30;

        AddLabel(
            group,
            "Skull Keys:",
            leftLabel,
            y + 5);

        skullKeysNumeric =
            CreateNumber(
                leftControl,
                y,
                99);

        AddLabel(
            group,
            "Gems:",
            rightLabel,
            y + 5);

        gemsNumeric =
            CreateNumber(
                rightControl,
                y,
                99);

        group.Controls.AddRange(
            new Control[]
            {
            foodNumeric,
            goldNumeric,

            torchesNumeric,
            keysNumeric,

            skullKeysNumeric,
            gemsNumeric
            });
    }


    private static void AddGroup(
    TableLayoutPanel layout,
    Control selector,
    string title,
    int column,
    int row)
    {
        var group =
            new GroupBox
            {
                Text =
                    title,

                Dock =
                    DockStyle.Top,

                Margin =
                    new Padding(8)
            };

        selector.Dock =
            DockStyle.Fill;

        group.Controls.Add(
            selector);

        layout.Controls.Add(
            group,
            column,
            row);
    }

    private static NumericUpDown CreateNumber(
    int x,
    int y,
    decimal maximum)
    {
        return new NumericUpDown
        {
            Left =
                x,

            Top =
                y,

            Width =
                110,

            Minimum =
                0,

            Maximum =
                maximum,

            ThousandsSeparator =
                true
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
                Text =
                    text,

                Left =
                    x,

                Top =
                    y,

                AutoSize =
                    true
            });
    }


    private void WireEvents()
    {
        spellSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetSpellQuantity(
                    currentSpell,
                    spellSelector.Quantity);

                currentSpell =
                    spellSelector.SelectedValue;

                ShowSpell();
            };

        reagentSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetReagentQuantity(
                    currentReagent,
                    reagentSelector.Quantity);

                currentReagent =
                    reagentSelector.SelectedValue;

                ShowReagent();
            };

        scrollSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetScrollQuantity(
                    currentScroll,
                    scrollSelector.Quantity);

                currentScroll =
                    scrollSelector.SelectedValue;

                ShowScroll();
            };

        potionSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetPotionQuantity(
                    currentPotion,
                    potionSelector.Quantity);

                currentPotion =
                    potionSelector.SelectedValue;

                ShowPotion();
            };

        weaponSelector.SelectionChanged +=
       (_, _) =>
       {
           if (loading ||
               save is null)
           {
               return;
           }

           save.SetWeaponQuantity(
               currentWeapon,
               weaponSelector.Quantity);

           currentWeapon =
               weaponSelector.SelectedValue;

           ShowWeapon();
       };

        armorSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetArmorQuantity(
                    currentArmor,
                    armorSelector.Quantity);

                currentArmor =
                    armorSelector.SelectedValue;

                ShowArmor();
            };

        helmSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetHelmQuantity(
                    currentHelm,
                    helmSelector.Quantity);

                currentHelm =
                    helmSelector.SelectedValue;

                ShowHelm();
            };

        shieldSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetShieldQuantity(
                    currentShield,
                    shieldSelector.Quantity);

                currentShield =
                    shieldSelector.SelectedValue;

                ShowShield();
            };

        ringSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetRingQuantity(
                    currentRing,
                    ringSelector.Quantity);

                currentRing =
                    ringSelector.SelectedValue;

                ShowRing();
            };

        amuletSelector.SelectionChanged +=
            (_, _) =>
            {
                if (loading ||
                    save is null)
                {
                    return;
                }

                save.SetAmuletQuantity(
                    currentAmulet,
                    amuletSelector.Quantity);

                currentAmulet =
                    amuletSelector.SelectedValue;

                ShowAmulet();
            };
    }

    public void LoadFromSave(
    Ultima5SaveFile saveFile)
    {
        save =
            saveFile;

        loading =
            true;

        currentSpell =
            SpellType.InLor;

        currentReagent =
            ReagentType.SulfurAsh;

        currentScroll =
            ScrollType.VasLor;

        currentPotion =
            PotionType.Blue;

        currentWeapon =
            WeaponType.Dagger;

        currentArmor =
            ArmorType.Cloth;

        currentHelm =
            HelmType.LeatherHelm;

        currentShield =
            ShieldType.SmallShield;

        currentRing =
            RingType.InvisibilityRing;

        currentAmulet =
            AmuletType.AmuletOfTurning;

        spellSelector.SelectedValue =
            currentSpell;

        reagentSelector.SelectedValue =
            currentReagent;

        scrollSelector.SelectedValue =
            currentScroll;

        potionSelector.SelectedValue =
            currentPotion;

        weaponSelector.SelectedValue =
            currentWeapon;

        armorSelector.SelectedValue =
            currentArmor;

        helmSelector.SelectedValue =
            currentHelm;

        shieldSelector.SelectedValue =
            currentShield;

        ringSelector.SelectedValue =
            currentRing;

        amuletSelector.SelectedValue =
            currentAmulet;

        foodNumeric.Value =
            save.Food;

        goldNumeric.Value =
            save.Gold;

        torchesNumeric.Value =
            save.Torches;

        keysNumeric.Value =
            save.Keys;

        skullKeysNumeric.Value =
            save.SkullKeys;

        gemsNumeric.Value =
            save.Gems;

        ShowAll();

        loading =
            false;
    }


    private void ShowAll()
    {
        ShowSpell();
        ShowReagent();
        ShowScroll();
        ShowPotion();

        ShowWeapon();
        ShowArmor();
        ShowHelm();
        ShowShield();

        ShowRing();
        ShowAmulet();
    }

    private void ShowSpell()
    {
        if (save is null)
            return;

        spellSelector.Quantity =
            save.GetSpellQuantity(
                currentSpell);
    }

    private void ShowReagent()
    {
        if (save is null)
            return;

        reagentSelector.Quantity =
            save.GetReagentQuantity(
                currentReagent);
    }

    private void ShowScroll()
    {
        if (save is null)
            return;

        scrollSelector.Quantity =
            save.GetScrollQuantity(
                currentScroll);
    }

    private void ShowPotion()
    {
        if (save is null)
            return;

        potionSelector.Quantity =
            save.GetPotionQuantity(
                currentPotion);
    }

    private void ShowWeapon()
    {
        if (save is null)
            return;

        weaponSelector.Quantity =
            save.GetWeaponQuantity(
                currentWeapon);
    }

    private void ShowArmor()
    {
        if (save is null)
            return;

        armorSelector.Quantity =
            save.GetArmorQuantity(
                currentArmor);
    }

    private void ShowHelm()
    {
        if (save is null)
            return;

        helmSelector.Quantity =
            save.GetHelmQuantity(
                currentHelm);
    }

    private void ShowShield()
    {
        if (save is null)
            return;

        shieldSelector.Quantity =
            save.GetShieldQuantity(
                currentShield);
    }

    private void ShowRing()
    {
        if (save is null)
            return;

        ringSelector.Quantity =
            save.GetRingQuantity(
                currentRing);
    }

    private void ShowAmulet()
    {
        if (save is null)
            return;

        amuletSelector.Quantity =
            save.GetAmuletQuantity(
                currentAmulet);
    }

    public void StoreToSave()
    {
        if (save is null)
            return;

        save.SetSpellQuantity(
            currentSpell,
            spellSelector.Quantity);

        save.SetReagentQuantity(
            currentReagent,
            reagentSelector.Quantity);

        save.SetScrollQuantity(
            currentScroll,
            scrollSelector.Quantity);

        save.SetPotionQuantity(
            currentPotion,
            potionSelector.Quantity);

        save.SetWeaponQuantity(
            currentWeapon,
            weaponSelector.Quantity);

        save.SetArmorQuantity(
            currentArmor,
            armorSelector.Quantity);

        save.SetHelmQuantity(
            currentHelm,
            helmSelector.Quantity);

        save.SetShieldQuantity(
            currentShield,
            shieldSelector.Quantity);

        save.SetRingQuantity(
            currentRing,
            ringSelector.Quantity);

        save.SetAmuletQuantity(
            currentAmulet,
            amuletSelector.Quantity);

        save.Food =
            (ushort)
        foodNumeric.Value;

        save.Gold =
            (ushort)
                goldNumeric.Value;

        save.Torches =
            (byte)
                torchesNumeric.Value;

        save.Keys =
            (byte)
                keysNumeric.Value;

        save.SkullKeys =
            (byte)
                skullKeysNumeric.Value;

        save.Gems =
            (byte)
                gemsNumeric.Value;
    }

}


