using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _iconImage;

    public Sprite HiddenIconSprite;

    public Sprite OpenedIconSprite;

    public bool IsSelected;

    public event Action<Card> OnClick;

    public void SetOpenedIconSprite(Sprite sprite)
    {
        OpenedIconSprite = sprite;
    }

    public void Show()
    {
        _iconImage.sprite = OpenedIconSprite;
        IsSelected = true;
    }

    public void Hide()
    {
        _iconImage.sprite = HiddenIconSprite;
        IsSelected = false;
    }

    public void OnCardClick()
    {
        OnClick?.Invoke(this);
    }
}   
