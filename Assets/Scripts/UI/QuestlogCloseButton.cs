using Naninovel;

/// <summary>
/// Close button for the quest log.
/// </summary>
public class QuestlogCloseButton : ScriptableLabeledButton
{
    private QuestlogPanel questlogPanel;

    protected override void Awake()
    {
        base.Awake();

        questlogPanel = GetComponentInParent<QuestlogPanel>();
    }

    protected override void OnButtonClick() => questlogPanel.Hide();
}
