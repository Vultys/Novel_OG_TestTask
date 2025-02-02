using UnityEngine;

[CreateAssetMenu(fileName = "MemoryCardGameSettings", menuName = "MemoryCardGameSettings", order = 0)]
public class MemoryCardGameSettings : ScriptableObject 
{
    public Sprite[] cardFaces;

    public Card cardPrefab;

    public GameObject gridTransform;
}