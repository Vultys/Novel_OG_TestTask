using System;
using Naninovel;
using UnityEngine;

/// <summary>
/// Represents a message recorded in <see cref="IQuestlogUI"/>.
/// </summary>
[Serializable]
public struct QuestEntity : IEquatable<QuestEntity>
{
    /// <summary>
    /// Text of the message.
    /// </summary>
    public LocalizableText Text => text;
    /// <summary>
    /// Whether the message has an associated actor.
    /// </summary>
    public bool Authored => !string.IsNullOrEmpty(author);
    /// <summary>
    /// Actor ID associated with the message when <see cref="Authored"/> or null.
    /// </summary>
    public string AuthorId => author;

    [SerializeField] private LocalizableText text;
    [SerializeField] private string author;

    public QuestEntity(LocalizableText text, string author = null)
    {
        this.text = text;
        this.author = author;
    }

    public bool Equals(QuestEntity other)
    {
        return text.Equals(other.text);
    }

    public override bool Equals(object obj)
    {
        return obj is QuestEntity other && Equals(other);
    }

    public override int GetHashCode()
    {
        return text.GetHashCode();
    }
}
