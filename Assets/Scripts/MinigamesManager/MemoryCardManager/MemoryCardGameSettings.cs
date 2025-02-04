using UnityEngine;

/// <summary>
/// Settings for the memory cards minigame.
/// </summary>
[CreateAssetMenu(fileName = "MemoryCardGameSettings", menuName = "MemoryCardGameSettings", order = 0)]
public class MemoryCardGameSettings : ScriptableObject 
{
    public Sprite[] cardFaces;

    public Card cardPrefab;

    public float checkDelay;
}