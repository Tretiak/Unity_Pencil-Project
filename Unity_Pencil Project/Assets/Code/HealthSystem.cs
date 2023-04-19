using System;
using UnityEngine;

namespace Code
{
    public class HealthSystem: MonoBehaviour
    {
        [SerializeField]
        private HealthBarUI _healthBarUI;
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = new HealthComponent();
        }

        private void Start()
        {
            _healthComponent.Init();
            _healthComponent.OnHealthChanged += HealthComponent_OnHealthChanged;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                _healthComponent.Heal(10f);
                
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                _healthComponent.ApplyDamage(10f);
               
            }
            
        }

        private void HealthComponent_OnHealthChanged()
        {
            _healthBarUI.UpdateHealthBar(_healthComponent.GetHealthNormalized());
        }

        private void OnDestroy()
        {
            _healthComponent.OnHealthChanged -= HealthComponent_OnHealthChanged;
        }
    }
}