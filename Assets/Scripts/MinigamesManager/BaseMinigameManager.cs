using System;
using UnityEngine;

public abstract class BaseMinigameManager : MonoBehaviour
{
    public abstract void StartMinigame();

    public abstract event Action OnMinigameFinished;
}