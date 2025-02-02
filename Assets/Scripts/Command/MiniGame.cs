using UnityEngine;

namespace Naninovel.Commands
{
    [CommandAlias("minigame")]
    public class MiniGame : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken)
        {
            // 1. Disable Naninovel input.
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = false;

            // 2. Stop script player.
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();

            // 3. Reset state.
            var stateManager = Engine.GetService<IStateManager>();
            await stateManager.ResetStateAsync();
        
            // 4. Switch cameras.
            var advCamera = GameObject.Find("AdvCamera").GetComponent<Camera>();
            advCamera.enabled = true;
            var naniCamera = Engine.GetService<ICameraManager>().Camera;
            naniCamera.enabled = false;

            // 4. Start minigame.
            var minigamesManager = Object.FindObjectOfType<MinigamesManager>();
            minigamesManager.StartMinigame(MinigameType.MemoryCards, asyncToken);
        }
    }
}