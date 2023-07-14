using System;
using Code.UI.Elements;
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
            _healthComponent.OnDie += OnDie;
        }

        private void OnDie()
        {
           Destroy(gameObject);
        }
        

        public HealthComponent GetHealth()
        {
            return _healthComponent;
        }

        private void HealthComponent_OnHealthChanged()
        {
            _healthBarUI.UpdateHealthBar(_healthComponent.GetHealthNormalized());
        }

        private void OnDestroy()
        {
            _healthComponent.OnHealthChanged -= HealthComponent_OnHealthChanged;
            _healthComponent.OnDie -= OnDie;
        }
    }
}