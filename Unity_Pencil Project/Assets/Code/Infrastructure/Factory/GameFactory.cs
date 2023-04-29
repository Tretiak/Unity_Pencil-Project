using System.Collections.Generic;
using Code.Character;
using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.States;
using Code.Logic.EnemySpawners;
using Code.StaticData;
using Code.StaticData.ScriptableObjects.EnemyStaticData;
using Code.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        
        private readonly IAssets _assets;
        private GameObject _createCharacter;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;

        private GameObject CharacterGameObject { get; set; }

        public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService persistentProgressService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
        }

        public GameObject CreateHud()
        {
            string path = AssetPath.HudPath;
            GameObject hud = InstantiateRegistered(path);
            hud.GetComponentInChildren<ScoreCounter>().Construct(_persistentProgressService.Progress.WorldData);
            return hud;
        }

        public GameObject CreateCharacter(GameObject at)
        {
            string characterPrefabPath = AssetPath.CharacterPath;
            Vector3 position = at.transform.position;
            GameObject character = InstantiateRegistered(characterPrefabPath, position);
            CharacterGameObject = character;
            return character;
        }

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.GetForEnemy(enemyTypeId);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);
            IHealth health = enemy.GetComponent<IHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;
            Transform characterTransform = CharacterGameObject.transform;

            enemy.GetComponent<ActorUI>().Construct(health);
            enemy.GetComponent<EnemyMoveToPlayer>().Construct(characterTransform);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
            EnemyAttack enemyAttack = enemy.GetComponent<EnemyAttack>();

            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(enemyData.MinLoot,enemyData.MaxLoot);
            lootSpawner.Construct(this, _randomService);

            enemyAttack.Construct(characterTransform);
            enemyAttack.Damage = enemyData.Damage;
            enemyAttack.AttackCooldown = enemyData.AttackCooldown;
            enemyAttack.CleavageRadius = enemyData.CleavageRadius;
            
            enemy.GetComponent<EnemyRotateToCharacter>()?.Construct(characterTransform);
            return enemy;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_persistentProgressService.Progress.WorldData);
            return lootPiece;
        }

        public void CreateSpawner(Vector3 at, string spawnerId, EnemyTypeId enemyTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.EnemyTypeId = enemyTypeId;
        }


        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string characterPrefabPath, Vector3 position)
        {
            GameObject gameobject = _assets.Instantiate(characterPrefabPath, atPosition: position);
            RegisterProgressWatchers(gameobject);
            return gameobject;
        }
        private GameObject InstantiateRegistered(string characterPrefabPath)
        {
            GameObject gameobject = _assets.Instantiate(characterPrefabPath);
            RegisterProgressWatchers(gameobject);
            return gameobject;
        }

        private void RegisterProgressWatchers(GameObject gameobject)
        {
            foreach (ISavedProgressReader progressReader in gameobject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            ProgressReaders.Add(progressReader);
        }
    }
}