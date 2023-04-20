using System;

namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public HealthState CharacterHealthState;
        public Stats CharacterStats;
        public KillData KillData;
        

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            CharacterHealthState = new HealthState();
            CharacterStats = new Stats();
            KillData = new KillData();

        }
    }

    [Serializable]
    public class Stats
    {
        public float Damage;
        public float DamageRadius;
    }
}