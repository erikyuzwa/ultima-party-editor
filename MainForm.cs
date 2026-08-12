using UltimaSaveEditor.Ultima4;

namespace ultima_party_editor
{
    public partial class MainForm : Form
    {
        private readonly UltimaIVEditorControl
        ultimaIVEditor;

        public MainForm()
        {
            InitializeComponent();

            ultimaIVEditor =
            new UltimaIVEditorControl();

            ultimaIVEditor.Dock =
                DockStyle.Fill;

            tabPage4.Controls.Add(
                ultimaIVEditor);
        }
    }
}
