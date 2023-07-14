using System;
using Code.Enemy;
using UnityEngine;

namespace Code.StaticData.ScriptableObjects.LevelStaticData
{
    [Serializable]
    public class EnemySpawnerStaticData
    {
        public string Id;
        public EnemyTypeId EnemyTypeId;
        public Vector3 Position;

        public EnemySpawnerStaticData(string id, EnemyTypeId enemyTypeId, Vector3 position)
        {
            Id = id;
            EnemyTypeId = enemyTypeId;
            Position = position;
        }
    }
}