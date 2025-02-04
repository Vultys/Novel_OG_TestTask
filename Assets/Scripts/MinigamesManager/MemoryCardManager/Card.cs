using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a memory card.
/// </summary>
public class Card : MonoBehaviour
{
    [SerializeField] private Image _iconImage;

    public Sprite HiddenIconSprite;

    public Sprite OpenedIconSprite;

    public bool IsSelected;

    public event Action<Card> OnClick;

    /// <summary>
    /// Sets the opened icon sprite.
    /// </summary>
    /// <param name="sprite"> The sprite to set as opened icon. </param>
    public void SetOpenedIconSprite(Sprite sprite)
    {
        OpenedIconSprite = sprite;
    }

    /// <summary>
    /// Shows the card.
    /// </summary>
    public void Show()
    {
        _iconImage.sprite = OpenedIconSprite;
        IsSelected = true;
    }

    /// <summary>
    /// Hides the card.
    /// </summary>
    public void Hide()
    {
        _iconImage.sprite = HiddenIconSprite;
        IsSelected = false;
    }

    /// <summary>
    /// Handles the click event.
    /// </summary>
    public void OnCardClick()
    {
        OnClick?.Invoke(this);
    }
}   
