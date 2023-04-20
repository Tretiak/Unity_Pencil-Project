using Code.Enemy;
using UnityEngine;

namespace Code.StaticData.ScriptableObjects.EnemyStaticData
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Scriptable objects/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;
        [Range(1,100)]
        public float Hp;
        [Range(1,50)]
        public float Damage;
        [Range(0.1f,5f)]
        public float AttackCooldown = 2f;
        [Range(0.5f,1f)]
        public float CleavageRadius = 0.5f;
        public GameObject Prefab;
        [Range(0, 20f)]
        public float MoveSpeed = 5;


    }
}