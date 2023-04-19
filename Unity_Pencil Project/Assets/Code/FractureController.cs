using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureController : MonoBehaviour
{
    [SerializeField]
    private Fracture _fracture;

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TriggerObjectFracture();
        }
    }

    public void TriggerObjectFracture()
    {
        
            _fracture.ComputeFracture();
            
    }
}
