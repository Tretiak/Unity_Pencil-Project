﻿using Code.Character;
using Code.Editor;
using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Logic;
using Code.StaticData;
using Code.UI;
using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using CharacterController = KinematicCharacterController.Examples.CharacterController;


namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain  loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
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
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Position,spawnerData.Id,spawnerData.EnemyTypeId);
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