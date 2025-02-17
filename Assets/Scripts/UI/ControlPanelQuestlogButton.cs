﻿using Naninovel;
using Naninovel.UI;

/// <summary>
/// Button for the quest log.
/// </summary>
public class ControlPanelQuestlogButton : ScriptableLabeledButton
{
    private IUIManager uiManager;

    protected override void Awake()
    {
        base.Awake();

        uiManager = Engine.GetService<IUIManager>();
    }
    
    protected override void OnButtonClick()
    {
        uiManager.GetUI<IPauseUI>()?.Hide();
        uiManager.GetUI<IQuestlogUI>()?.Show();
    }
}
