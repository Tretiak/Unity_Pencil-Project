using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
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