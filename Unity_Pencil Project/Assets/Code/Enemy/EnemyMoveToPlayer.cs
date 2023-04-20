using System;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class EnemyMoveToPlayer : EnemyFollow
    {
        public NavMeshAgent Agent;
        private Transform _characterTransform;
        private const float minDistance = 1f;
        private IGameFactory _gameFactory;

        public void Construct(Transform characterTransform)
        {
            _characterTransform = characterTransform;
        }
        
        private void Update()
        {
            if(CharacterTransformInitialized() && CharacterNotReached())
                Agent.destination = _characterTransform.position;
        }

        private bool CharacterTransformInitialized()
        {
            return _characterTransform!=null;
        }
        

        private bool CharacterNotReached()
        {
            return Vector3.Distance(Agent.transform.position, _characterTransform.transform.position) >=  minDistance;
        }
    }
}