using System;
using Code.Infrastructure.States;
using Code.UI;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper: MonoBehaviour , ICoroutineRunner
    {
        public LoadingCurtain LoadingCurtainPrefab;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(LoadingCurtainPrefab));
            _game._stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}