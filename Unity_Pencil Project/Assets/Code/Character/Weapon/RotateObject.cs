using System;
using UnityEngine;

namespace Code.Character
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField]
        private float _degreesPerSecond = 10;

        private void Update()
        {
            transform.Rotate(new Vector3(0, _degreesPerSecond, 0) * Time.deltaTime);
        }
    }
}