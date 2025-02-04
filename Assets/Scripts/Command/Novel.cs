using UnityEngine;

namespace Naninovel.Commands
{
    /// <summary>
    /// Switches to novel mode from minigame mode.
    /// </summary>
    [CommandAlias("novel")]
    public class Novel : Command
    {
        public StringParameter ScriptPath;

        public StringParameter Label;

        public override async UniTask ExecuteAsync(AsyncToken asyncToken)
        {
            DisableMinigameCamera();
            EnableNaniCamera();
            
            if (Assigned(ScriptPath))
            {
                var scriptPlayer = Engine.GetService<IScriptPlayer>();
                await scriptPlayer.PreloadAndPlayAsync(ScriptPath, Label);
            }
            
            EnableInput();
        }

        /// <summary>
        /// Disables the minigame camera.
        /// </summary>
        private void DisableMinigameCamera()
        {
            var minigameCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();
            if (minigameCamera != null)
                minigameCamera.enabled = false;
        }
    
        /// <summary>
        /// Enables the Naninovel camera.
        /// </summary>
        private void EnableNaniCamera()
        {
            var naniCamera = Engine.GetService<ICameraManager>()?.Camera;
            if (naniCamera != null)
                naniCamera.enabled = true;
        }

        /// <summary>
        /// Enables the Naninovel input.
        /// </summary>
        private void EnableInput()
        {
            var inputManager = Engine.GetService<IInputManager>();
            if (inputManager != null)
                inputManager.ProcessInput = true;
        }
    }
}
