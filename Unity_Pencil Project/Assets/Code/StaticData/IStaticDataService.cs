using Code.Enemy;
using Code.Infrastructure.Services;
using Code.Logic.EnemySpawners;
using Code.StaticData.ScriptableObjects.EnemyStaticData;

namespace Code.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData GetForEnemy(EnemyTypeId enemyTypeId);
        LevelStaticData ForLevel(string sceneKey);
        
    }
}