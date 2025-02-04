using System;

/// <summary>
/// Base class for minigame managers.
/// </summary>
public abstract class BaseMinigameManager
{
    /// <summary>
    /// Starts the minigame.
    /// </summary>
    public abstract void StartMinigame();

    /// <summary>
    /// Event that is called when the minigame is finished.
    /// </summary>
    public abstract event Action OnMinigameFinished;
}