namespace UltimaSaveEditor.Common;

public sealed class EnumQuantitySelector<T>
    : UserControl
    where T : struct, Enum
{
    private readonly ComboBox combo;
    private readonly NumericUpDown quantityNumeric;

    private bool loading;

    public event EventHandler? SelectionChanged;

    public T SelectedValue
    {
        get
        {
            T[] values =
                Enum.GetValues<T>();

            int index =
                combo.SelectedIndex;

            if (index < 0 ||
                index >= values.Length)
            {
                return values[0];
            }

            return values[index];
        }

        set
        {
            T[] values =
                Enum.GetValues<T>();

            combo.SelectedIndex =
                Array.IndexOf(
                    values,
                    value);
        }
    }

    public byte Quantity
    {
        get =>
            (byte)
                quantityNumeric.Value;

        set =>
            quantityNumeric.Value =
                Math.Min(
                    value,
                    (byte)99);
    }

    public EnumQuantitySelector(
        string itemLabel)
    {
        Height = 50;

        var itemText =
            new Label
            {
                Text = itemLabel + ":",
                Left = 10,
                Top = 10,
                Width = 55
            };

        combo =
            new ComboBox
            {
                Left = 65,
                Top = 9,
                Width = 140,

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

        var quantityLabel =
            new Label
            {
                Text = "Qty:",
                Left = 230,
                Top = 10,
                Width = 35
            };

        quantityNumeric =
            new NumericUpDown
            {
                Left = 260,
                Top = 9,

                Width = 55,

                Minimum = 0,
                Maximum = 99,

                TextAlign =
                    HorizontalAlignment.Right
            };

        Controls.AddRange(
            new Control[]
            {
                itemText,
                combo,
                quantityLabel,
                quantityNumeric
            });

        combo.SelectedIndexChanged +=
            (_, _) =>
            {
                if (!loading)
                {
                    SelectionChanged?.Invoke(
                        this,
                        EventArgs.Empty);
                }
            };

        if (combo.Items.Count > 0)
        {
            loading = true;

            combo.SelectedIndex = 0;

            loading = false;
        }
    }
}