using Naninovel;

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
