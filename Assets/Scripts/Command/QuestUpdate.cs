namespace Naninovel.Commands
{
    [CommandAlias("quest")]
    public class Quest : Command
    {
        public LocalizableTextParameter Author;

        public LocalizableTextParameter Description;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var questlogUI = Engine.GetService<IUIManager>().GetUI<IQuestlogUI>();
            if (questlogUI is null) return default;
            questlogUI.AddMessage(Description.Value, Author);
            return default;
        }
    }
}