using System;
using System.Collections;
using Code.Data;
using TMPro;
using UnityEngine;

namespace Code.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject LootVisual;
        public GameObject PickupFXPrefab;
        public TextMeshProUGUI LootText;
        public GameObject PickupPopup;
        
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;
        private float _waitTimeBeforeDestroy = 1.5f;


        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other)
        {
            Pickup();
        }

        private void Pickup()
        {
            if(_picked)
                return;

            _picked = true;
            
            //UpdateWorldData();
            HideVisual();
            PlayPickupFX();
            ShowText();
            Destroy(gameObject,_waitTimeBeforeDestroy);
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideVisual()
        {
            LootVisual.SetActive(false);
        }
        

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }

        private void PlayPickupFX()
        {
            Instantiate(PickupFXPrefab, transform.position, Quaternion.identity);
        }
    }
}