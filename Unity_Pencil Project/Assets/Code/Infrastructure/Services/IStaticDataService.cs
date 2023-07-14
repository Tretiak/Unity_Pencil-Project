using Code.Enemy;
using Code.Infrastructure.Services;
using Code.StaticData.ScriptableObjects.EnemyStaticData;
using Code.StaticData.ScriptableObjects.WindowsStaticData;
using Code.UI.Services.Windows;
using UnityEngine;

namespace Code.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData GetForEnemy(EnemyTypeId enemyTypeId);
        WindowConfig ForWindow(WindowId shop);
        
    }
}