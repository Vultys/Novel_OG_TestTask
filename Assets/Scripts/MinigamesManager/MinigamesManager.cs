using Naninovel;
using Naninovel.Commands;
using UnityEngine;

/// <summary>
/// Manages the minigames.
/// </summary>
public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private MemoryCardGameSettings _memoryCardSettings;
    
    [SerializeField] private Transform _memoryCardsContainer;

    private BaseMinigameManager _currentMinigame;

    private const string _dialogNameAfterMemoryGame = "RewardingDialog";

    public static MinigamesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Starts the minigame.
    /// </summary>
    /// <param name="minigameType"> The type of the minigame to start. </param>
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

    /// <summary>
    /// Finishes the memory card game.
    /// </summary>
    public void FinishedMemoryCardGame()
    {
        var switchCommand = new Novel { ScriptPath = _dialogNameAfterMemoryGame };
        switchCommand.ExecuteAsync(default).Forget();
    }
}