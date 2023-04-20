using System.Collections.Generic;
using System.Linq;
using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.StaticData.ScriptableObjects.EnemyStaticData;
using UnityEngine;

namespace Code.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId,EnemyStaticData> _enemies;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>(AssetPath.EnemyStaticDataPath).ToDictionary(x=> x.EnemyTypeId, x=> x);
        }

        public EnemyStaticData GetForEnemy(EnemyTypeId enemyTypeId)
        {
            return _enemies.TryGetValue(enemyTypeId, out EnemyStaticData enemyStaticData) ? enemyStaticData : null;
        }
    }
}