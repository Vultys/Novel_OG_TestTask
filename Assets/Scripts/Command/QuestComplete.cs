namespace Naninovel.Commands
{
    [CommandAlias("questComplete")]
    public class QuestComplete : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var questlogUI = Engine.GetService<IUIManager>().GetUI<IQuestlogUI>();
            if (questlogUI is null) return default;
            questlogUI.Clear();
            return default;
        }
    }
}