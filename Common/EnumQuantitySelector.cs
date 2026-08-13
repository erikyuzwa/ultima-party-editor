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
        Height = 90;

        var itemText =
            new Label
            {
                Text = itemLabel,
                Left = 15,
                Top = 20,
                Width = 80
            };

        combo =
            new ComboBox
            {
                Left = 100,
                Top = 15,
                Width = 205,
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
                Text = "Quantity:",
                Left = 15,
                Top = 57,
                Width = 80
            };

        quantityNumeric =
            new NumericUpDown
            {
                Left = 100,
                Top = 52,
                Width = 100,
                Minimum = 0,
                Maximum = 99
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