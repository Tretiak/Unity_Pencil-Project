using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Character
{
    public class HpBar : MonoBehaviour
    {
        public Image CurrentImage;

        public void SetValue(float current, float max) => CurrentImage.fillAmount = current / max;
    }
}