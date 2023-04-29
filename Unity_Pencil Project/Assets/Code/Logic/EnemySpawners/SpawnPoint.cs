using Code.Data;
using Code.Enemy;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class SpawnPoint: MonoBehaviour, ISavedProgress
    {
        public EnemyTypeId EnemyTypeId;

        public string Id { get; set; }

        [SerializeField] 
        private bool _defeated;

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        public bool Defeated => _defeated;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }
        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(Id))
                _defeated = true;
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
          GameObject enemy = _factory.CreateEnemy(EnemyTypeId, transform);
          _enemyDeath = enemy.GetComponent<EnemyDeath>();
          _enemyDeath.OnDeath += EnemyDefeated;
        }

        private void EnemyDefeated()
        {
            if (_enemyDeath != null)
            {
                _enemyDeath.OnDeath -= EnemyDefeated;
            }
            
            _defeated = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_defeated)
            {
                playerProgress.KillData.ClearedSpawners.Add(Id);
            }
        }
    }

    public enum EnemyTypeId
    {
        Pen = 0,
        Scissors = 10,
    }
}