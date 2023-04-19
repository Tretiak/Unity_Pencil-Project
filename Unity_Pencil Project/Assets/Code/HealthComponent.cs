using System;
using UnityEngine;

namespace Code
{ 
    public class HealthComponent

    {

        public event Action OnHealthChanged;
        
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
            Debug.Log(_health +"after Heal");
        }

        public void ApplyDamage(float damageAmount)
        {
            _health -= damageAmount;
            InvokeHealthChangedEvent();
            Debug.Log(_health + "after Damage");
        }

        public float GetHealthNormalized()
        {
            float healthNormalized = _health / _healthMax;
            Debug.Log(healthNormalized + "Health normalized");
            return healthNormalized;
        }
    }
}