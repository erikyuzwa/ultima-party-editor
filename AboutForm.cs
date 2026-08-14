using System.Reflection;
using System.Diagnostics;
using UltimaSaveEditor.Common;

namespace ultima_party_editor;

public sealed class AboutForm
    : Form
{
    public AboutForm()
    {
        Text =
            "About Ultima Party Editor";

        StartPosition =
            FormStartPosition.CenterParent;

        FormBorderStyle =
            FormBorderStyle.FixedDialog;

        MaximizeBox =
            false;

        MinimizeBox =
            false;

        ShowInTaskbar =
            false;

        ClientSize =
            new Size(
                480,
                380);

        BuildLayout();
    }

    private void BuildLayout()
    {
        var titleLabel =
            new Label
            {
                Text =
                    "Ultima Party Editor",

                Font =
                    new Font(
                        Font.FontFamily,
                        16,
                        FontStyle.Bold),

                AutoSize =
                    true,

                Left =
                    25,

                Top =
                    25
            };

        var versionLabel =
            new Label
            {
                Text =
                     $"Version {AppVersion.Version}",

                AutoSize =
                    true,

                Left =
                    27,

                Top =
                    65
            };

        var descriptionLabel =
            new Label
            {
                Text =
                    "A save game editor for Ultima I, II, III, IV and V.",

                AutoSize =
                    true,

                Left =
                    27,

                Top =
                    100
            };

        var copyrightLabel =
            new Label
            {
                Text =
                    "Ultima, Lord British and related names are trademarks of their respective owners.",

                AutoSize =
                    true,

                Left =
                    27,

                Top =
                    135
            };

        var closeButton =
            new Button
            {
                Text = "OK",

                Width = 90,
                Height = 30,

                Left = 325,
                Top = 330,

                DialogResult =
                    DialogResult.OK
            };

        var creditsGroup =
        new GroupBox
        {
            Text = "Credits",
            Left = 25,
            Top = 175,
            Width = 390,
            Height = 150
        };

        AddCreditLink(
            creditsGroup,
            "Ultima Codex",
            "https://wiki.ultimacodex.com/",
            25,
            30);

        AddCreditLink(
            creditsGroup,
            "Ultima Save Game Format Documentation",
            "https://martin.brenner.de/ultima/",
            25,
            60);

        AddCreditLink(
            creditsGroup,
            "Xenerkes Dragon's existing Ultima Savegame Editor",
            "https://ultima1.ultimacodex.com/the-ultima-savegame-editor/",
            25,
            90);

        AddCreditLink(
            creditsGroup,
            "Project Github",
            "https://github.com/erikyuzwa/ultima-party-editor.git",
            25,
            120);


        Controls.AddRange(
            new Control[]
            {
                titleLabel,
                versionLabel,
                descriptionLabel,
                copyrightLabel,
                creditsGroup,
                closeButton
            });

        AcceptButton =
            closeButton;

        CancelButton =
            closeButton;
    }

    private static void AddCreditLink(
    Control parent,
    string text,
    string url,
    int x,
    int y)
    {
        var link =
            new LinkLabel
            {
                Text = text,

                Left = x,
                Top = y,

                AutoSize = true,

                Tag = url
            };

        link.LinkClicked +=
            CreditLink_LinkClicked;

        parent.Controls.Add(
            link);
    }

    private static void CreditLink_LinkClicked(
    object? sender,
    LinkLabelLinkClickedEventArgs e)
    {
        if (sender is not LinkLabel link)
            return;

        if (link.Tag is not string url)
            return;

        try
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
        }
        catch
        {
            MessageBox.Show(
                $"Unable to open:\n{url}",
                "Unable to Open Link",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}