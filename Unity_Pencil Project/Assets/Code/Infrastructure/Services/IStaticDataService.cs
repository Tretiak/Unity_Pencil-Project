using Code.Enemy;
using Code.Infrastructure.Services;
using Code.StaticData.ScriptableObjects.EnemyStaticData;

namespace Code.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData GetForEnemy(EnemyTypeId enemyTypeId);
    }
}