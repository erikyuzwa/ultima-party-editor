using System.Reflection;

namespace UltimaSaveEditor.Common;

public static class AppVersion
{
    public static string Version
    {
        get
        {
            Assembly assembly =
                typeof(AppVersion).Assembly;

            AssemblyInformationalVersionAttribute? attribute =
                assembly.GetCustomAttribute<
                    AssemblyInformationalVersionAttribute>();

            return attribute?.InformationalVersion
                ?? "Unknown";
        }
    }
}