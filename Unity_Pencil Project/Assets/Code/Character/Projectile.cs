using System;
using System.Collections;
using System.Collections.Generic;
using Code.Editor;
using Code.Enemy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject _hitFx;
    private bool _isCollided;
    private float _damage;

    public void Init(float damage)
    {
        _damage = damage;
    }


    private void OnTriggerEnter(Collider collided)
    {
        if (CheckWallsTag(collided) && FirstCollision())
        {
            _isCollided = true;
            SpawnHitFX();
            Destroy(gameObject);

        } else if (CheckEnemyTag(collided) && FirstCollision())
        {
            _isCollided = true;
            SpawnHitFX();
            collided.GetComponentInParent<IHealth>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

   

    private bool FirstCollision()
    {
        return !_isCollided;
    }

    private static bool CheckEnemyTag(Collider other)
    {
        return other.gameObject.CompareTag(TagsExtension.Enemy);
    }

    private static bool CheckWallsTag(Collider other)
    {
        return other.gameObject.CompareTag(TagsExtension.Environment);
    }

    private void SpawnHitFX()
    {
        Instantiate(_hitFx, transform.position, Quaternion.identity, null);
    }
}
