using System;
using Code.Enemy;
using KinematicCharacterController.Examples;
using UnityEngine;

namespace Code.Character
{
    public class CharacterDeath : MonoBehaviour
    {
        [SerializeField] private CharacterHealth CharacterHealth;
        [SerializeField] private CharacterMove _characterMove;
        [SerializeField] private CharacterAttack _characterAttack;
        [SerializeField] private CharacterAnimator characterAnimator;
        [SerializeField] private GameObject _deathFX;
        private bool _isDead;

        private void Start()
        {
            CharacterHealth.HealthChanged += HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && CharacterHealth.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            _characterMove.enabled = false;
            _characterAttack.enabled = false;
            characterAnimator.PlayDeathAnimation();
            Instantiate(_deathFX, transform.position, Quaternion.identity);
        }

        private void OnDestroy()
        {
            CharacterHealth.HealthChanged -= HealthChanged;
        }
    }
}