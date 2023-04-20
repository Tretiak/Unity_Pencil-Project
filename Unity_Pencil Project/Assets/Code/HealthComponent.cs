using System;
using UnityEngine;

namespace Code
{ 
    public class HealthComponent

    {

        public event Action OnHealthChanged;
        public event Action OnDie;
        
        private float _health;
        private float _healthMax = 100;

        public void Init()
        {
            _health = _healthMax;
        }

        private void InvokeHealthChangedEvent()
        {
            OnHealthChanged?.Invoke();
        }

        public void Heal(float healAmount)
        {
            
            _health += healAmount;
            InvokeHealthChangedEvent();
            
        }

        private void CheckHealth()
        {
            if (_health <= 0) OnDie?.Invoke();
        }

        public void ApplyDamage(float damageAmount)
        {
            CheckHealth();
            _health -= damageAmount;
            InvokeHealthChangedEvent();
            
        }

        public float GetHealthNormalized()
        {
            float healthNormalized = _health / _healthMax;
            return healthNormalized;
        }
    }
}