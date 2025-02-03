using UnityEngine;

namespace Naninovel.Commands
{
    [CommandAlias("novel")]
    public class Novel : Command
    {
        public StringParameter ScriptPath;
        public StringParameter Label;

        public override async UniTask ExecuteAsync(AsyncToken asyncToken)
        {
            var minigameCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            minigameCamera.enabled = false;
            var naniCamera = Engine.GetService<ICameraManager>().Camera;
            naniCamera.enabled = true;
            
            if (Assigned(ScriptPath))
            {
                var scriptPlayer = Engine.GetService<IScriptPlayer>();
                await scriptPlayer.PreloadAndPlayAsync(ScriptPath, Label);
            }
            
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = true;
        }
    }
}
