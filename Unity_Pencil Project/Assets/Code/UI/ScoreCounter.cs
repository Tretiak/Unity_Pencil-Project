using System;
using Code.Data;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _score.text = $"{_worldData.LootData.Collected}";
        }
    }
}