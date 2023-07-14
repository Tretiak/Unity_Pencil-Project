using System.Collections.Generic;
using System.Linq;
using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.StaticData.ScriptableObjects.EnemyStaticData;
using Code.StaticData.ScriptableObjects.LevelStaticData;
using Code.StaticData.ScriptableObjects.WindowsStaticData;
using Code.UI.Services.Windows;
using UnityEngine;

namespace Code.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId,EnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>(AssetPath.EnemyStaticDataPath).ToDictionary(x=> x.EnemyTypeId, x=> x);
            _levels = Resources.LoadAll<LevelStaticData>(AssetPath.LevelStaticDataPath).ToDictionary(x => x.LevelKey, x => x);
            _windowConfigs = Resources.Load<WindowStaticData>(AssetPath.WindowsStaticData)
                .Configs
                .ToDictionary(x=> x.WindowId, x=> x);
        }

        public EnemyStaticData GetForEnemy(EnemyTypeId enemyTypeId)
        {
            return _enemies.TryGetValue(enemyTypeId, out EnemyStaticData enemyStaticData) ? enemyStaticData : null;
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) ? windowConfig : null;;
        }
    }
}