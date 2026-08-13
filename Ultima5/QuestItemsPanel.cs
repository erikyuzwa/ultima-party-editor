namespace UltimaSaveEditor.Ultima5;

public sealed class QuestItemsPanel
    : UserControl
{
    public QuestItemsPanel()
    {
        Dock =
            DockStyle.Fill;

        Padding =
            new Padding(20);

        Controls.Add(
            new Label
            {
                Text =
                    "Ultima V quest item editor will be added here.",

                AutoSize = true,
                Left = 20,
                Top = 20
            });
    }

    public void LoadFromSave(
        Ultima5SaveFile save)
    {
    }

    public void StoreToSave()
    {
    }
}