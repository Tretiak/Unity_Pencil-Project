using System;
using System.Linq;
using Code.Character;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using UnityEditor;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        public EnemyAnimator EnemyAnimator;
        public float AttackCooldown = 2f;
        public float CleavageRadius = 0.5f;
        public float Damage = 10f;
        public Transform AttackPoint;
        
        private Transform _characterTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];

        private bool _attackIsActive;

        public void Construct(Transform characterTransform)
        {
            _characterTransform = characterTransform;
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
            {
                StartAttack();
            }
        }

        public void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(AttackPoint.position,CleavageRadius,.2f);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        public void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        private bool Hit(out Collider hit)
        {
          int hitCount = Physics.OverlapSphereNonAlloc(AttackPoint.position, CleavageRadius, _hits, _layerMask);

          hit = _hits.FirstOrDefault();
          
          return hitCount > 0;
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        private void StartAttack()
        {
            transform.LookAt(_characterTransform);
            EnemyAnimator.PlayAttack();
            _isAttacking = true;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _attackCooldown -= Time.deltaTime;
            }
        }

        private bool CanAttack()
        {
            return _attackIsActive && !_isAttacking && CooldownIsUp();
        }

        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0;
        }
        
        
    }
}