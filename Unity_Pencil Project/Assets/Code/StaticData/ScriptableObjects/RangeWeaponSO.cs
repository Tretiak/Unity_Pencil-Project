using UnityEngine;

namespace Code.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Range Weapon", menuName = "Scriptable objects")]
    public class RangeWeaponSO : WeaponSO
    {
        public GameObject Projectile;
        public float ProjectileSpeed;
        public float Damage;
    }

    public class WeaponSO : ScriptableObject
    {
        
    }
}