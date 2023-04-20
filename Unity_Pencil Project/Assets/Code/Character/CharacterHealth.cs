using System;
using Code.Data;
using Code.Enemy;
using Code.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Code.Character
{
    public class CharacterHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public CharacterAnimator _animator;
        public event Action HealthChanged;
        
        private HealthState _healthState;

        public float Current { 
            get=>_healthState.CurrentHP; 
            set
            {
                if (_healthState.CurrentHP != value)
                {
                    _healthState.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max { get=>_healthState.MaxHP; set => _healthState.MaxHP = value; }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _healthState = playerProgress.CharacterHealthState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.CharacterHealthState.CurrentHP = Current;
            playerProgress.CharacterHealthState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if(Current <= 0)
                return;
            
            Current -= damage;
            // Play hit Anim
        }
    }
}