using UltimaSaveEditor.Common;
using UltimaSaveEditor.Ultima1;
using UltimaSaveEditor.Ultima2;
using UltimaSaveEditor.Ultima3;
using UltimaSaveEditor.Ultima4;

namespace ultima_party_editor
{
    public partial class MainForm : Form
    {
        private readonly Ultima1EditorControl ultima1Editor;
        private readonly Ultima2EditorControl ultima2Editor;
        private readonly Ultima3EditorControl ultima3Editor;
        private readonly Ultima4EditorControl ultima4Editor;

        public MainForm()
        {
            InitializeComponent();

            Text = "Ultima Save Editor";

            ClientSize = new Size(800, 680);

            ultima1Editor = new Ultima1EditorControl();
            ultima2Editor = new Ultima2EditorControl();
            ultima3Editor = new Ultima3EditorControl();
            ultima4Editor = new Ultima4EditorControl();

            ultima1Editor.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(ultima1Editor);
            tabPage1.Tag = ultima1Editor;

            ultima2Editor.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(ultima2Editor);
            tabPage2.Tag = ultima2Editor;

            ultima3Editor.Dock = DockStyle.Fill;
            tabPage3.Controls.Add(ultima3Editor);
            tabPage3.Tag = ultima3Editor;

            ultima4Editor.Dock = DockStyle.Fill;
            tabPage4.Controls.Add(ultima4Editor);
            tabPage4.Tag = ultima4Editor;
        }


        private IGameEditor? GetCurrentEditor()
        {
            return tabControl1
                .SelectedTab?
                .Tag as IGameEditor;
        }

        private void OpenCurrentSave()
        {
            IGameEditor? editor =
                GetCurrentEditor();

            if (editor is null)
                return;

            using OpenFileDialog dialog =
                new();

            dialog.Title =
                $"Open {editor.GameName} Save";

            dialog.Filter =
                "Save Files (*.sav)|*.sav|" +
                "All Files (*.*)|*.*";

            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            if (dialog.ShowDialog(this)
                != DialogResult.OK)
            {
                return;
            }

            try
            {
                editor.OpenSave(
                    dialog.FileName);

                UpdateWindowTitle();

                SetStatus(
                    $"Loaded {dialog.FileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Open Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                SetStatus(
                    "Open failed.");
            }
        }

        private void SaveCurrent()
        {
            IGameEditor? editor =
                GetCurrentEditor();

            if (editor is null ||
                !editor.IsLoaded)
            {
                SetStatus(
                    "No save file is currently loaded.");

                return;
            }

            try
            {
                editor.Save();

                UpdateWindowTitle();

                SetStatus(
                    $"Saved {editor.Filename}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Save Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                SetStatus(
                "Save failed.");
            }
        }

        private void SaveCurrentAs()
        {
            IGameEditor? editor =
                GetCurrentEditor();

            if (editor is null ||
                !editor.IsLoaded)
            {
                SetStatus(
            "No save file is currently loaded.");

                return;
            }

            using SaveFileDialog dialog =
                new();

            dialog.Title =
                $"Save {editor.GameName} Save As";

            dialog.Filter =
                "Save Files (*.sav)|*.sav|" +
                "All Files (*.*)|*.*";

            dialog.FileName =
                editor.Filename is not null
                    ? Path.GetFileName(
                        editor.Filename)
                    : "PARTY.SAV";

            dialog.OverwritePrompt = true;

            if (dialog.ShowDialog(this)
                != DialogResult.OK)
            {
                return;
            }

            try
            {
                editor.SaveAs(
                    dialog.FileName);

                UpdateWindowTitle();

                SetStatus(
                    $"Saved as {dialog.FileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Save Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                SetStatus(
                    "Save As failed.");
            }
        }

        private void UpdateWindowTitle()
        {
            IGameEditor? editor =
                GetCurrentEditor();

            if (editor is null)
            {
                Text =
                    "Ultima Save Editor";

                return;
            }

            if (!editor.IsLoaded ||
                editor.Filename is null)
            {
                Text =
                    $"Ultima Save Editor - " +
                    $"{editor.GameName}";

                return;
            }

            Text =
                $"Ultima Save Editor - " +
                $"{editor.GameName} - " +
                $"{Path.GetFileName(editor.Filename)}";
        }

        private void openToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            OpenCurrentSave();
        }

        private void saveToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            SaveCurrent();
        }

        private void saveAsToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            SaveCurrentAs();
        }

        private void exitToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void SetStatus(string message)
        {
            statusLabel.Text = message;
        }
    }

}
