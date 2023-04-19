using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class HealthBarUI: MonoBehaviour
    {
        [SerializeField] private Image _healthBarFill;
        public void UpdateHealthBar(float fillAmount)
        {
            _healthBarFill.fillAmount = fillAmount;
        }
    }
}