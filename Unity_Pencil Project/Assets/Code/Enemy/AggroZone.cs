using System;
using System.Collections;
using UnityEngine;

namespace Code.Enemy
{
    public class AggroZone : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public EnemyFollow Follow;
        public float Cooldown;
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;
            SwitchFollowOff();
        }

        private void OnTriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();

                SwitchFollowOn();
            }
            
        }

        private void OnTriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                SwitchFollowOff();
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
            
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(Cooldown);
                SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            Follow.enabled = true;
        }

        private void SwitchFollowOff()
        {
            Follow.enabled = false;
        }

        private void OnDestroy()
        {
            TriggerObserver.TriggerEnter -= OnTriggerEnter;
            TriggerObserver.TriggerExit -= OnTriggerExit;
        }
    }
}