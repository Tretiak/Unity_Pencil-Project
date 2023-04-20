using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemyRotateToCharacter : EnemyFollow
    {
            public float RotationSpeed;
        
            private Transform _characterTransform;
            private Vector3 _positionToLook;
           
            public void Construct(Transform characterTransform)
            {
              _characterTransform = characterTransform;
            }
            
            private void Update()
            {
              if (IsInitialized())
                RotateTowardsCharacter();
            }
            
        
            private void RotateTowardsCharacter()
            {
              UpdatePositionToLookAt();
        
              transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
            }
        
            private void UpdatePositionToLookAt()
            {
              Vector3 positionDelta = _characterTransform.position - transform.position;
              _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
            }
            
            private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
              Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        
            private Quaternion TargetRotation(Vector3 position) =>
              Quaternion.LookRotation(position);
        
            private float SpeedFactor() =>
              RotationSpeed * Time.deltaTime;
        
            private bool IsInitialized() => 
              _characterTransform != null;
            
        
         
    }
}