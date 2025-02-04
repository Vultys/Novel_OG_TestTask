namespace Naninovel.Commands
{
    /// <summary>
    /// Adds a quest to the quest log.
    /// </summary>
    [CommandAlias("quest")]
    public class Quest : Command
    {
        public LocalizableTextParameter Author;

        public LocalizableTextParameter Description;
        
        /// <summary>
        /// Adds a quest to the quest log.
        /// </summary>
        /// <param name="asyncToken"> The token to monitor for cancellation requests. </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var questlogUI = Engine.GetService<IUIManager>().GetUI<IQuestlogUI>();
            if (questlogUI is null) return default;
            questlogUI.AddMessage(Description.Value, Author);
            return default;
        }
    }
}