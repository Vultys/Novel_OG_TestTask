using System;
using Naninovel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestEntityUI : ScriptableUIBehaviour
{
    [Serializable]
    private class OnMessageChangedEvent : UnityEvent<string> { }

    [Serializable]
    private class OnAuthorChangedEvent : UnityEvent<string> { }

    protected virtual LocalizableText Text { get; private set; }
    protected virtual string AuthorId { get; private set; }
    protected virtual GameObject AuthorPanel => authorPanel;

    [SerializeField] private GameObject authorPanel;
    [SerializeField] private OnMessageChangedEvent onMessageChanged;
    [SerializeField] private OnAuthorChangedEvent onAuthorChanged;
    [SerializeField] private Image _backgroundImage;

    private ICharacterManager charManager => Engine.GetService<ICharacterManager>();

    public virtual QuestEntity GetState() => new QuestEntity(Text, AuthorId);

    public virtual void Initialize(QuestEntity message)
    {
        SetText(message.Text);
        SetAuthor(message.AuthorId);
    }

    public virtual void MakeInactive()
    {
        _backgroundImage.color = Color.black;
    }

    public virtual void Append(LocalizableText text)
    {
        SetText(Text + text);
    }

    protected virtual void SetText(LocalizableText text)
    {
        Text = text;
        onMessageChanged?.Invoke(text);
    }

    protected virtual void SetAuthor(string authorId)
    {
        AuthorId = authorId;
        var name = charManager.GetDisplayName(authorId) ?? authorId;
        if (AuthorPanel) AuthorPanel.SetActive(!string.IsNullOrWhiteSpace(name));
        onAuthorChanged?.Invoke(name);
    }
}

