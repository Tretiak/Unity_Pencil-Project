using Code.Data;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.States;

namespace Code.Infrastructure
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private const string GameScene = "GameScene_01";

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var playerProgress = new PlayerProgress(GameScene);
            playerProgress.CharacterHealthState.MaxHP = 50;
            playerProgress.CharacterHealthState.ResetHP();
            playerProgress.CharacterStats.Damage = 10;
            playerProgress.CharacterStats.DamageRadius = 0.5f;
            
            
            return playerProgress;
        }

        public void Exit()
        {
            
        }
    }
}