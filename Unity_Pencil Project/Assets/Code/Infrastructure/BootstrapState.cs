using System;

namespace Code.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private const string GameScene = "GameScene_01";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameScene);
        }

        private void RegisterServices()
        {
            
        }

        public void Exit()
        {
        }
    }
}