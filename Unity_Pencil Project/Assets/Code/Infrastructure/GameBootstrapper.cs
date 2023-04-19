using System;
using Code.UI;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper: MonoBehaviour , ICoroutineRunner
    {
        public LoadingCurtain LoadingCurtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, LoadingCurtain);
            _game._stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}