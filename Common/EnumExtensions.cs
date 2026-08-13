using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace UltimaSaveEditor.Common;

public static class EnumExtensions
{
    public static string ToDisplayName(
        this Enum value)
    {
        string name =
            value.ToString();

        FieldInfo? field =
            value.GetType()
                .GetField(name);

        if (field is not null)
        {
            DescriptionAttribute? attribute =
                field.GetCustomAttribute<
                    DescriptionAttribute>();

            if (attribute is not null)
            {
                return attribute.Description;
            }
        }

        return SplitPascalCase(name);
    }

    private static string SplitPascalCase(
        string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        StringBuilder result =
            new();

        result.Append(value[0]);

        for (int i = 1;
             i < value.Length;
             i++)
        {
            char current =
                value[i];

            char previous =
                value[i - 1];

            if (char.IsUpper(current) &&
                !char.IsUpper(previous))
            {
                result.Append(' ');
            }

            result.Append(current);
        }

        return result.ToString();
    }
}