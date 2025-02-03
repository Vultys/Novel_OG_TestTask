using Naninovel;
using Naninovel.Commands;
using UnityEngine;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private MemoryCardGameSettings _memoryCardSettings;
    
    [SerializeField] private Transform _memoryCardsContainer;

    private BaseMinigameManager _currentMinigame;

    private readonly string _dialogNameAfterMemoryGame = "RewardingDialog";

    public void StartMinigame(MinigameType minigameType)
    {
        switch (minigameType)
        {
            case MinigameType.MemoryCards:
                _currentMinigame = new MemoryCardManager(_memoryCardSettings, _memoryCardsContainer);
                _currentMinigame.StartMinigame();
                _currentMinigame.OnMinigameFinished += FinishedMemoryCardGame;
                break;
            default: break;
        }
    }

    public void FinishedMemoryCardGame()
    {
        var switchCommand = new Novel { ScriptPath = _dialogNameAfterMemoryGame };
        switchCommand.ExecuteAsync(default).Forget();
    }
}