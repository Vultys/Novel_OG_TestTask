using UnityEngine;

namespace Naninovel.Commands
{
    [CommandAlias("minigame")]
    public class MiniGame : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken)
        {
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = false;

            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();

            var minigameCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            minigameCamera.enabled = true;
            var naniCamera = Engine.GetService<ICameraManager>().Camera;
            naniCamera.enabled = false;

            var minigamesManager = Object.FindObjectOfType<MinigamesManager>();
            minigamesManager.StartMinigame(MinigameType.MemoryCards);
        }
    }
}