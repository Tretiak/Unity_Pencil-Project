using System;
using System.Collections.Generic;
using Code.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Character
{
    public class WeaponHolder: MonoBehaviour
    {

    public RangeWeaponSO RangeWeaponData;
    public TriggerObserver TriggerObserver;
   

    private void Start()
    {
        TriggerObserver.TriggerEnter += OnTriggerEnter;
        TriggerObserver.TriggerExit += OnTriggerExit;
        
    }

    private void OnTriggerExit(Collider obj)
    {
        
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.TryGetComponent(out IRangeWeapon weapon))
        {
            weapon.LoadWeapon(RangeWeaponData);
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        TriggerObserver.TriggerEnter -= OnTriggerEnter;
        TriggerObserver.TriggerExit -= OnTriggerExit;
    }
    }
}