using System;
using Naninovel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Represents a message recorded in <see cref="IQuestlogUI"/>.
/// </summary>
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

    /// <summary>
    /// Initializes the UI with the provided message.
    /// </summary>
    /// <param name="message"> The message to initialize the UI with. </param>
    public virtual void Initialize(QuestEntity message)
    {
        SetText(message.Text);
        SetAuthor(message.AuthorId);
    }

    /// <summary>
    /// Makes the quest part completed
    /// </summary>
    public virtual void MakeCompleted()
    {
        _backgroundImage.color = Color.black;
    }

    /// <summary>
    /// Makes the quest part incomplete
    /// </summary>
    /// <param name="text"> The text to set as the quest part. </param>
    public virtual void Append(LocalizableText text)
    {
        SetText(Text + text);
    }

    /// <summary>
    /// Sets the quest part text.
    /// </summary>
    /// <param name="text"> The text to set as the quest part. </param>
    protected virtual void SetText(LocalizableText text)
    {
        Text = text;
        onMessageChanged?.Invoke(text);
    }

    /// <summary>
    /// Sets the quest part author.
    /// </summary>
    /// <param name="authorId"> The author ID to set as the quest part. </param>
    protected virtual void SetAuthor(string authorId)
    {
        AuthorId = authorId;
        var name = charManager.GetDisplayName(authorId) ?? authorId;
        if (AuthorPanel) AuthorPanel.SetActive(!string.IsNullOrWhiteSpace(name));
        onAuthorChanged?.Invoke(name);
    }
}

