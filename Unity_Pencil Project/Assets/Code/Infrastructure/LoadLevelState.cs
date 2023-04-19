using Code.UI;
using KinematicCharacterController.Examples;
using UnityEngine;
using CharacterController = KinematicCharacterController.Examples.CharacterController;


namespace Code.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string CharacterPath = "Character/Character";
        private const string PlayerPath = "Character/Player";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain  loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
            
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject character = Instantiate(CharacterPath, atPosition: initialPoint.transform.position);
            CharacterController characterController = character.GetComponent<CharacterController>();
            GameObject player = Instantiate(PlayerPath);
            Player playerInput = player.GetComponent<Player>();
            CharacterCamera characterCamera = Camera.main.GetComponent<CharacterCamera>();
            playerInput.Init(characterController,characterCamera);
            
            _stateMachine.Enter<GameLoopState>();

        }

        private static void CameraFollow(GameObject character)
        {
            Camera.main.GetComponent<CharacterCamera>().SetFollowTransform(character.transform);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        private static GameObject Instantiate(string path, Vector3 atPosition)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, atPosition,Quaternion.identity);
        }
    }
}