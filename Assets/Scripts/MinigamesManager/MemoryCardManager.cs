using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCardManager : BaseMinigameManager
{
	private MemoryCardGameSettings _settings;

	private List<Sprite> _spritePairs;

	private Card _firstSelected;

	private Card _secondSelected;

	private int _matchCounts;

	private Transform _panel;

	private MinigamesManager _minigamesManager;

	public MemoryCardManager(MinigamesManager minigamesManager, MemoryCardGameSettings memoryCardGameSettings, Transform panel)
	{
		_settings = memoryCardGameSettings;
		_panel = panel;
		_minigamesManager = minigamesManager;
	}

    public override void StartMinigame()
    {
		PrepareSprites();
		CreateCards();
    }

	public void SetSelectedCard(Card card)
	{
		if(!card.IsSelected) 
		{
			card.Show();

			if(_firstSelected == null)
			{
				_firstSelected = card;
				return;
			}

			if(_secondSelected == null)
			{
				_secondSelected = card;
				_minigamesManager.StartCoroutine(CheckMatching(_firstSelected, _secondSelected));
				_firstSelected = null;
				_secondSelected = null;
			}
		}
	}

	private IEnumerator CheckMatching(Card a, Card b)
	{
		yield return new WaitForSeconds(0.3f);

		if(a.OpenedIconSprite == b.OpenedIconSprite)
		{
			_matchCounts++;
			if(_matchCounts >= _spritePairs.Count / 2)
			{
				_minigamesManager.OnMemoryCardsGameOver();
				_panel.gameObject.SetActive(false);
			}
		}
		else
		{
			a.Hide();
			b.Hide();
		}
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
		for(int i = 0; i < _spritePairs.Count; i++)
		{
			Card card = Instantiate(_settings.cardPrefab, _panel);
			card.SetOpenedIconSprite(_spritePairs[i]);
			card.OnClick += SetSelectedCard;
		}
	}

	private void ShuffleSprites(List<Sprite> spritesList)
	{
		for(int i = spritesList.Count - 1; i > 0; i--)
		{
			int randomIndex = Random.Range(0, i + 1);

			Sprite temp = spritesList[i];
			spritesList[i] = spritesList[randomIndex];
			spritesList[randomIndex] = temp;
		}
	}
}