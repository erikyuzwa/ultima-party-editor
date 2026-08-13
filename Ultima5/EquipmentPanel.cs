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

    private Ultima5SaveFile? save;

    private bool loading;

    public EquipmentPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

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
                    5
            };

        layout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

        layout.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50));

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

        Controls.Add(
            layout);
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
                    DockStyle.Fill,

                Height =
                    115,

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


    //private void WireEvents()
    //{
    //    spellSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreSpell();

    //            ShowSpell();
    //        };

    //    reagentSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreReagent();

    //            ShowReagent();
    //        };

    //    scrollSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreScroll();

    //            ShowScroll();
    //        };

    //    potionSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StorePotion();

    //            ShowPotion();
    //        };

    //    weaponSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreWeapon();

    //            ShowWeapon();
    //        };

    //    armorSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreArmor();

    //            ShowArmor();
    //        };

    //    helmSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreHelm();

    //            ShowHelm();
    //        };

    //    shieldSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreShield();

    //            ShowShield();
    //        };

    //    ringSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreRing();

    //            ShowRing();
    //        };

    //    amuletSelector.SelectionChanged +=
    //        (_, _) =>
    //        {
    //            if (loading)
    //                return;

    //            StoreAmulet();

    //            ShowAmulet();
    //        };
    //}

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
    }

}


