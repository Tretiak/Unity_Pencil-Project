using System;
using Code.Data;
using Code.Enemy;
using Code.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Code.Character
{
    public class CharacterAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField]
        private CharacterAnimator _characterAnimator;

        [SerializeField]
        private Transform _attackPoint;

        [SerializeField]
        private float _radius;

        [SerializeField] 
        private static int maxNumberOfTargetsPerHit = 5;

        private static int _layerMask;

        private Collider[] _hits = new Collider[maxNumberOfTargetsPerHit];
        private Stats _characterStats;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_characterAnimator.IsAttacking)
            {
               _characterAnimator.PlayAttackAnimation();
            }
      
            if (Input.GetMouseButtonDown(1))
            {
                _characterAnimator.PlayPowerAttackAnimation();
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_characterStats.Damage);
            }
        }

        private int Hit()
        {
           return Physics.OverlapSphereNonAlloc(_attackPoint.position, _characterStats.DamageRadius, _hits, _layerMask);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _characterStats = playerProgress.CharacterStats;
        }
    }
}