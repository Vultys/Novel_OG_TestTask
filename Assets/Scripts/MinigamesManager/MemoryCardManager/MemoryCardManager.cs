using System;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

/// <summary>
/// Manages the memory cards minigame.
/// </summary>
public class MemoryCardManager : BaseMinigameManager
{
	private readonly MemoryCardGameSettings _settings;

	private readonly Transform _container;

	private List<Sprite> _spritePairs;

	private List<Card> _cardList = new List<Card>();

	private Card _firstSelected, _secondSelected;

	private int _matchCounts;

	public override event Action OnMinigameFinished;

	/// <summary>
	/// Creates a new instance of <see cref="MemoryCardManager"/>.
	/// </summary>
	/// <param name="memoryCardGameSettings">The settings for the memory cards minigame.</param>
	/// <param name="container">The container for the memory cards.</param>
	public MemoryCardManager(MemoryCardGameSettings memoryCardGameSettings, Transform container)
	{
		_settings = memoryCardGameSettings;
		_container = container;
	}

	/// <summary>
	/// Starts the memory cards minigame.
	/// </summary>
	public override void StartMinigame()
	{
		_container.gameObject.SetActive(true);
		PrepareSprites();
		CreateCards();
	}

	/// <summary>
	/// Sets the selected card.
	/// </summary>
	/// <param name="card">The card to set as selected.</param>
	public void SetSelectedCard(Card card)
	{
		if (card.IsSelected) return;

		if (_firstSelected == null)
		{
			card.Show();
			_firstSelected = card;
		}
		else if (_secondSelected == null)
		{
			card.Show();
			_secondSelected = card;
			CheckMatching().Forget();
		}
	}

	/// <summary>
	/// Checks if the selected cards match.
	/// </summary>
	/// <returns> A task that represents the asynchronous operation. </returns>
	private async UniTaskVoid CheckMatching()
	{
		if (_firstSelected.OpenedIconSprite == _secondSelected.OpenedIconSprite)
		{
			if (++_matchCounts >= _spritePairs.Count / 2)
			{
				_container.gameObject.SetActive(false);
				DestroyCards();
				OnMinigameFinished?.Invoke();
			}
		}
		else
		{
			await UniTask.Delay(TimeSpan.FromSeconds(_settings.checkDelay));
			_firstSelected.Hide();
			_secondSelected.Hide();
		}

		ResetSelectedCards();
	}

	/// <summary>
	/// Resets the selected cards.
	/// </summary>
	private void ResetSelectedCards()
	{
		_firstSelected = null;
		_secondSelected = null;
	}

	/// <summary>
	/// Prepares the sprites for the cards.
	/// </summary>
	private void PrepareSprites()
	{
		_spritePairs = new List<Sprite>(_settings.cardFaces.Length * 2);

		foreach (var face in _settings.cardFaces)
        {
            _spritePairs.Add(face);
            _spritePairs.Add(face);
        }

		ShuffleSprites(_spritePairs);
	}

	/// <summary>
	/// Creates the cards.
	/// </summary>
	private void CreateCards()
	{
		foreach (var sprite in _spritePairs)
        {
			Card card = UnityEngine.Object.Instantiate(_settings.cardPrefab, _container);
			_cardList.Add(card);
			card.SetOpenedIconSprite(sprite);
			card.OnClick += SetSelectedCard;
		}
	}

	/// <summary>
	/// Creates the cards.
	/// </summary>
	private void DestroyCards()
	{
		foreach (var card in _cardList)
        {
			card.OnClick -= SetSelectedCard;
			UnityEngine.Object.Destroy(card.gameObject);
		}
		_cardList.Clear();
	}

	/// <summary>
	/// Shuffles the sprites.
	/// </summary>
	/// <param name="spritesList"> The sprites list to shuffle. </param>
	private void ShuffleSprites(List<Sprite> spritesList)
	{
		for (int i = spritesList.Count - 1; i > 0; i--)
		{
			int randomIndex = UnityEngine.Random.Range(0, i + 1);

			Sprite temp = spritesList[i];
			spritesList[i] = spritesList[randomIndex];
			spritesList[randomIndex] = temp;
		}
	}
}