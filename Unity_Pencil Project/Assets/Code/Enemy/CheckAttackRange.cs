using System;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttack EnemyAttack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;
            EnemyAttack.DisableAttack();
        }

        private void OnTriggerExit(Collider obj)
        {
            EnemyAttack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            EnemyAttack.EnableAttack();
        }

        private void OnDestroy()
        {
            TriggerObserver.TriggerEnter -= OnTriggerEnter;
            TriggerObserver.TriggerExit -= OnTriggerExit;
        }
    }
}