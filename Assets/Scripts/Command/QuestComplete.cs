namespace Naninovel.Commands
{
    /// <summary>
    /// Clears the quest log.
    /// </summary>
    [CommandAlias("questComplete")]
    public class QuestComplete : Command
    {
        /// <summary>
        /// Clears the quest log.
        /// </summary>
        /// <param name="asyncToken"> The token to monitor for cancellation requests. </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var questlogUI = Engine.GetService<IUIManager>().GetUI<IQuestlogUI>();
            if (questlogUI is null) return default;
            questlogUI.Clear();
            return default;
        }
    }
}