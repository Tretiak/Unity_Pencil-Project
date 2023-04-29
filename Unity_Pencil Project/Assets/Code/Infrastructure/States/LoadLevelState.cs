using Code.Character;
using Code.Editor;
using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Logic;
using Code.UI;
using KinematicCharacterController.Examples;
using UnityEngine;
using CharacterController = KinematicCharacterController.Examples.CharacterController;


namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain  loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
            
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();
            InitLootPieces();
            GameObject character = _gameFactory.CreateCharacter(GameObject.FindWithTag(TagsExtension.InitialPointTag));
            InitCharacterControl(character);
            InitHud(character);
        }
        private void InitLootPieces()
        {
            foreach (string key in _progressService.Progress.WorldData.LootData.LootPiecesOnScene.Dictionary.Keys)
            {
                LootPiece lootPiece = _gameFactory.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = key;
            }
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerGameObject in GameObject.FindGameObjectsWithTag(TagsExtension.EnemySpawnerTag))
            { 
                EnemySpawner spawner = spawnerGameObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private void InitHud(GameObject character)
        {
           GameObject hud = _gameFactory.CreateHud();
           ActorUI actorUI = hud.GetComponentInChildren<ActorUI>();
           actorUI.Construct(character.GetComponent<IHealth>());
        }

        private void InitCharacterControl(GameObject character)
        {
            CharacterController characterController = character.GetComponent<CharacterController>();
            CharacterMove characterMove = character.GetComponent<CharacterMove>();
            CharacterCamera characterCamera = Camera.main.GetComponent<CharacterCamera>();
            characterMove.Init(characterController, characterCamera);
        }

        private static void CameraFollow(GameObject character)
        {
            Camera.main.GetComponent<CharacterCamera>().SetFollowTransform(character.transform);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        
    }
}