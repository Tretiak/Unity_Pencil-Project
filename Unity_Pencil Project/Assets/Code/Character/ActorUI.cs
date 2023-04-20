﻿using System;
using Code.Enemy;
using UnityEngine;

namespace Code.Character
{
    public class ActorUI: MonoBehaviour
    {
        public HpBar HpBar;

        private IHealth _health;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
      
            if(health != null)
                Construct(health);
        }

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;

        }

        private void OnDestroy()
        {
            if(_health != null)
            _health.HealthChanged -= UpdateHpBar;
        }
        
        private void UpdateHpBar()
        {
            HpBar.SetValue(_health.Current,_health.Max);
        }
    }
}