using System;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyMoveToPlayer _enemyMoveToPlayer;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private GameObject _deathFX;
        [SerializeField] private float _timeBeforeBodyCleanUp = 3f;

        public event Action OnDeath;

        private void Start()
        {
            _enemyHealth.HealthChanged += HealthChanged;
        }

        private void HealthChanged()
        {
            if (_enemyHealth.Current <= 0) Die();
        }

        private void Die()
        {
            DisableComponents();
            _enemyHealth.HealthChanged -= HealthChanged;
            _enemyAnimator.PlayDeath();


            Instantiate(_deathFX, transform.position, Quaternion.identity);
            Destroy(gameObject, _timeBeforeBodyCleanUp);
            OnDeath?.Invoke();
        }

        private void DisableComponents()
        {
            _enemyAttack.enabled = false;
            _enemyMoveToPlayer.enabled = false;
        }

        private void OnDestroy()
        {
            _enemyHealth.HealthChanged -= HealthChanged;
        }
    }
}