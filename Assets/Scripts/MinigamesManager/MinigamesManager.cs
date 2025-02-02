using Naninovel;
using Naninovel.Commands;
using UnityEngine;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private MemoryCardGameSettings _memoryCardSettings;
    
    [SerializeField] private Transform _memoryCardsGameOverPanel;

    private BaseMinigameManager _currentMinigame;

    private AsyncToken _asyncToken;

    public void StartMinigame(MinigameType minigameType, AsyncToken asyncToken)
    {
        switch (minigameType)
        {
            case MinigameType.MemoryCards:
                _currentMinigame = new MemoryCardManager(this,_memoryCardSettings, _memoryCardsGameOverPanel);
                _currentMinigame.StartMinigame();
                _asyncToken = asyncToken;
                break;
            default: break;
        }
    }

    public void OnMemoryCardsGameOver()
    {
        var switchCommand = new Novel { ScriptPath = "RewardingDialog" };
        switchCommand.ExecuteAsync(_asyncToken).Forget();
    }
}