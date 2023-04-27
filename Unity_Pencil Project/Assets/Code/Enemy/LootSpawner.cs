using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.States;
using UnityEngine;

namespace Code.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;
        private IRandomService _randomService;

        public void Construct(IGameFactory factory, IRandomService randomService)
        {
            _randomService = randomService;
            _factory = factory;
        }

        private void Start()
        {
            _enemyDeath.OnDeath += SpawnLoot;
        }

        private void SpawnLoot()
        {
            _enemyDeath.OnDeath -= SpawnLoot;
           LootPiece loot = _factory.CreateLoot();
           loot.transform.position = transform.position;
           
           Loot lootItem = GenerateLoot();
           loot.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            Loot loot = new Loot
            {
                Value = _randomService.Next(_lootMin,_lootMax)
            };
            return loot;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}