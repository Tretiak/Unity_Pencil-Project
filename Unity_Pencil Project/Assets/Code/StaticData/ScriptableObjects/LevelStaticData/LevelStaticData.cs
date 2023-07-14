using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.ScriptableObjects.LevelStaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerStaticData> EnemySpawners;
    }
}