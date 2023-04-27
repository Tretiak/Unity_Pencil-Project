using System.Collections.Generic;
using Code.Enemy;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateCharacter(GameObject at);

        GameObject CreateHud();

        void Register(ISavedProgressReader progressReader);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        
        void Cleanup();
        GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
        LootPiece CreateLoot();
    }
}