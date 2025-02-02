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
            var advCamera = GameObject.Find("AdvCamera").GetComponent<Camera>();
            advCamera.enabled = false;
            var naniCamera = Engine.GetService<ICameraManager>().Camera;
            naniCamera.enabled = true;
            
            // 3. Load and play specified script (if assigned).
            if (Assigned(ScriptPath))
            {
                var scriptPlayer = Engine.GetService<IScriptPlayer>();
                await scriptPlayer.PreloadAndPlayAsync(ScriptPath, Label);
            }

            // 4. Enable Naninovel input.
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = true;
        }
    }
}
