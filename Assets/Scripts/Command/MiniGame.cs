using UnityEngine;

namespace Naninovel.Commands
{
    /// <summary>
    /// Starts the minigame.
    /// </summary>
    [CommandAlias("minigame")]
    public class MiniGame : Command
    {
        /// <summary>
        /// Starts the minigame.
        /// </summary>
        /// <param name="asyncToken"> The token to monitor for cancellation requests. </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public override async UniTask ExecuteAsync(AsyncToken asyncToken)
        {
            DisableInput();
            StopCurrentScript();

            EnableMinigameCamera();
            DisableNaniCamera();

            MinigamesManager.Instance.StartMinigame(MinigameType.MemoryCards);
        }

        /// <summary>
        /// Disables the Naninovel input.
        /// </summary>
        private void DisableInput()
        {
            var inputManager = Engine.GetService<IInputManager>();
            if (inputManager != null)
                inputManager.ProcessInput = false;
        }

        /// <summary>
        /// Stops the current Naninovel script.
        /// </summary>
        private void StopCurrentScript()
        {
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer?.Stop();
        }

        /// <summary>
        /// Enables the minigame camera.
        /// </summary>
        private void EnableMinigameCamera()
        {
            var minigameCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();
            if (minigameCamera != null)
                minigameCamera.enabled = true;
        }

        /// <summary>
        /// Disables the Naninovel camera.
        /// </summary>
        private void DisableNaniCamera()
        {
            var naniCamera = Engine.GetService<ICameraManager>()?.Camera;
            if (naniCamera != null)
                naniCamera.enabled = false;
        }
    }
}