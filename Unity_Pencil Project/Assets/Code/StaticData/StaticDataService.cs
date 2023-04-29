using System.Collections.Generic;
using System.Linq;
using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Logic.EnemySpawners;
using Code.StaticData.ScriptableObjects.EnemyStaticData;
using UnityEngine;

namespace Code.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId,EnemyStaticData> _enemies;
        private Dictionary<string,LevelStaticData> _levels;

        public void Load()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>(AssetPath.EnemyStaticDataPath)
                .ToDictionary(x=> x.EnemyTypeId, x=> x);
            
            _levels = Resources.LoadAll<LevelStaticData>(AssetPath.LevelStaticDataPath)
                .ToDictionary(x=> x.LevelKey, x=> x);
        }

        public EnemyStaticData GetForEnemy(EnemyTypeId enemyTypeId)
        {
            return _enemies.TryGetValue(enemyTypeId, out EnemyStaticData enemyStaticData) ? enemyStaticData : null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            return _levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData) ? levelStaticData : null;
        }
    }
}