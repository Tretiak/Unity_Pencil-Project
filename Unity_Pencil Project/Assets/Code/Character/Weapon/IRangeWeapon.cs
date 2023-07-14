using Code.StaticData.ScriptableObjects;
using UnityEngine;

namespace Code.Character
{
    
    public interface IRangeWeapon : IWeaponLoader
    {
        
    }

    public interface IWeaponLoader
    {
        void LoadWeapon(RangeWeaponSO weapon);
    }
}