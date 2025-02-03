using System.Collections.Generic;
using Naninovel;
using Naninovel.UI;

public interface IQuestlogUI : IManagedUI
{
    /// <summary>
    /// Adds message to the log.
    /// </summary>
    /// <param name="text">Text of the message.</param>
    /// <param name="authorId">ID of the actor associated with the message or null.</param>
    void AddMessage(LocalizableText text, string authorId = null);

    /// <summary>
    /// Appends text to the last message of the log (if exists).
    /// </summary>
    /// <param name="text">Text to append to the last message.</param>
    void AppendMessage(LocalizableText text);

    /// <summary>
    /// Removes all messages from the backlog.
    /// </summary>
    void Clear();
}
