using System;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(Die);
        }
    }
}