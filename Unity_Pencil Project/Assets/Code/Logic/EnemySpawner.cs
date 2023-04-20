using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemySpawner: MonoBehaviour, ISavedProgress
    {
        public EnemyTypeId EnemyTypeId;

        [SerializeField] 
        private bool _defeated;
        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        public bool Defeated => _defeated;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();

        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
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
                playerProgress.KillData.ClearedSpawners.Add(_id);
            }
        }
    }

    public enum EnemyTypeId
    {
        Pen = 0,
        Scissors = 10,
    }
}