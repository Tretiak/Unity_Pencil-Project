﻿using System;
using System.Collections.Generic;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.States;
using Code.UI;
using Code.UI.Services.Factory;

namespace Code.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type,IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this,
                    sceneLoader,
                    loadingCurtain, 
                    services.Single<IGameFactory>(), 
                    services.Single<IPersistentProgressService>(),
                    services.Single<IUIFactory>()),
                [typeof(LoadProgressState)]  = new LoadProgressState(this,
                    services.Single<IPersistentProgressService>(), 
                    services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };

        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
    }
}