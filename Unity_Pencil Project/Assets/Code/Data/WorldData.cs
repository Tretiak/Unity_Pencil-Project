using System;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;


        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}