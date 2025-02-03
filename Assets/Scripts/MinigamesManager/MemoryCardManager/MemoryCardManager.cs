using System;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

public class MemoryCardManager : BaseMinigameManager
{
	private MemoryCardGameSettings _settings;

	private List<Sprite> _spritePairs;

	private Card _firstSelected;

	private Card _secondSelected;

	private int _matchCounts;

	private Transform _container;

	public override event Action OnMinigameFinished;

	public MemoryCardManager(MemoryCardGameSettings memoryCardGameSettings, Transform container)
	{
		_settings = memoryCardGameSettings;
		_container = container;
	}

	public override void StartMinigame()
	{
		PrepareSprites();
		CreateCards();
	}

	public void SetSelectedCard(Card card)
	{
		if (!card.IsSelected)
		{
			if (_firstSelected == null)
			{
				card.Show();
				_firstSelected = card;
				return;
			}

			if (_secondSelected == null)
			{
				card.Show();
				_secondSelected = card;
				CheckMatching(_firstSelected, _secondSelected);
			}
		}
	}

	private async void CheckMatching(Card a, Card b)
	{
		if (a.OpenedIconSprite == b.OpenedIconSprite)
		{
			_matchCounts++;
			if (_matchCounts >= _spritePairs.Count / 2)
			{
				OnMinigameFinished?.Invoke();
				_container.gameObject.SetActive(false);
			}
		}
		else
		{
			await UniTask.Delay(TimeSpan.FromSeconds(_settings.checkDelay));
			a.Hide();
			b.Hide();
		}

		ResetSelectedCards();
	}

	private void ResetSelectedCards()
	{
		_firstSelected = null;
		_secondSelected = null;
	}

	private void PrepareSprites()
	{
		_spritePairs = new List<Sprite>();

		for (int i = 0; i < _settings.cardFaces.Length; i++)
		{
			_spritePairs.Add(_settings.cardFaces[i]);
			_spritePairs.Add(_settings.cardFaces[i]);
		}

		ShuffleSprites(_spritePairs);
	}

	private void CreateCards()
	{
		for (int i = 0; i < _spritePairs.Count; i++)
		{
			Card card = Instantiate(_settings.cardPrefab, _container);
			card.SetOpenedIconSprite(_spritePairs[i]);
			card.OnClick += SetSelectedCard;
		}
	}

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