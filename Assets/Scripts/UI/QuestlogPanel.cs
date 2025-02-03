using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

public class QuestlogPanel : CustomUI, IQuestlogUI, ILocalizableUI
{
    [Serializable]
    public new class GameState
    {
        public List<QuestEntity> Messages;
    }

    protected virtual QuestEntityUI LastMessage => messages.Last?.Value;
    protected virtual RectTransform MessagesContainer => messagesContainer;
    protected virtual ScrollRect ScrollRect => scrollRect;
    protected virtual QuestEntityUI MessagePrefab => messagePrefab;

    [SerializeField] private RectTransform messagesContainer;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private QuestEntityUI messagePrefab;
    [SerializeField] private int capacity = 300;
    [SerializeField] private int saveCapacity = 30;

    private readonly LinkedList<QuestEntityUI> messages = new LinkedList<QuestEntityUI>();
    private readonly Stack<QuestEntityUI> messagesPool = new Stack<QuestEntityUI>();
    private IInputManager inputManager;

    public virtual void AddMessage(LocalizableText text, string actorId = null)
    {
        if(LastMessage) LastMessage.MakeInactive();
        SpawnMessage(new QuestEntity(text, actorId));
    }

    public virtual void AppendMessage(LocalizableText text)
    {
        if (LastMessage) LastMessage.Append(text);
    }

    public virtual void Clear()
    {
        foreach (var message in messages)
        {
            message.gameObject.SetActive(false);
            messagesPool.Push(message);
        }
        messages.Clear();
    }

    protected override void Awake()
    {
        base.Awake();
        this.AssertRequiredObjects(messagesContainer, scrollRect, messagePrefab);

        inputManager = Engine.GetService<IInputManager>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (inputManager.TryGetSampler(InputNames.ShowBacklog, out var show))
            show.OnStart += Show;
        if (inputManager.TryGetSampler(InputNames.Cancel, out var cancel))
            cancel.OnEnd += Hide;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (inputManager.TryGetSampler(InputNames.ShowBacklog, out var show))
            show.OnStart -= Show;
        if (inputManager.TryGetSampler(InputNames.Cancel, out var cancel))
            cancel.OnEnd -= Hide;
    }

    protected virtual void SpawnMessage(QuestEntity message)
    {
        var messageUI = default(QuestEntityUI);

        if (messages.Count > capacity)
        {
            messageUI = messages.First.Value;
            messageUI.gameObject.SetActive(true);
            messageUI.transform.SetSiblingIndex(MessagesContainer.childCount - 1);
            messages.RemoveFirst();
            messages.AddLast(messageUI);
        }
        else
        {
            if (messagesPool.Count > 0)
            {
                messageUI = messagesPool.Pop();
                messageUI.gameObject.SetActive(true);
                messageUI.transform.SetSiblingIndex(MessagesContainer.childCount - 1);
            }
            else messageUI = Instantiate(MessagePrefab, MessagesContainer, false);

            messages.AddLast(messageUI);
        }

        messageUI.Initialize(message);
    }

    protected override void HandleVisibilityChanged(bool visible)
    {
        base.HandleVisibilityChanged(visible);

        MessagesContainer.gameObject.SetActive(visible);
    }

    protected override void SerializeState(GameStateMap stateMap)
    {
        base.SerializeState(stateMap);
        var state = new GameState
        {
            Messages = messages.TakeLast(saveCapacity).Select(m => m.GetState()).ToList()
        };
        stateMap.SetState(state);
    }

    protected override async UniTask DeserializeState(GameStateMap stateMap)
    {
        await base.DeserializeState(stateMap);

        Clear();

        var state = stateMap.GetState<GameState>();
        if (state is null) return;

        if (state.Messages?.Count > 0)
            foreach (var message in state.Messages)
                SpawnMessage(message);
    }

    protected override GameObject FindFocusObject()
    {
        var message = messages.Last;
        while (message != null)
        {
            if (message.Value.GetComponentInChildren<Selectable>() is Selectable selectable)
                return selectable.gameObject;
            message = message.Previous;
        }
        return null;
    }
}
