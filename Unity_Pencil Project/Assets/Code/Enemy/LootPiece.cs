using System;
using System.Collections;
using Code.Data;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Logic;
using TMPro;
using UnityEngine;

namespace Code.Enemy
{
    public class LootPiece : MonoBehaviour, ISavedProgress
    {
        public GameObject LootVisual;
        public GameObject PickupFXPrefab;
        public TextMeshProUGUI LootText;
        public GameObject PickupPopup;
        
        private string _id;
        private Loot _loot;
        private bool _picked;
        private bool _loadedFromProgress;
        private WorldData _worldData;
        private float _waitTimeBeforeDestroy = 1.5f;


        private void Start()
        {
            if(!_loadedFromProgress)
                _id = GetComponent<UniqueId>().Id;
        }

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
            if (!_picked)
            {
                _picked = true;
                Pickup();
            }
        }

        private void Pickup()
        {
            
            UpdateWorldData();
            HideVisual();
            PlayPickupFX();
            ShowText();
            Destroy(gameObject,_waitTimeBeforeDestroy);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _id = GetComponent<UniqueId>().Id;
            LootPieceData data = playerProgress.WorldData.LootData.LootPiecesOnScene.Dictionary[_id];
            Initialize(data.Loot);
            transform.position = data.Position.AsUnityVector3();

            _loadedFromProgress = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_picked)
                return;

            LootPieceDataDictionary lootPiecesOnScene = playerProgress.WorldData.LootData.LootPiecesOnScene;

            if (!lootPiecesOnScene.Dictionary.ContainsKey(_id))
                lootPiecesOnScene.Dictionary
                    .Add(_id, new LootPieceData(transform.position.AsVector3Data(), _loot));
        }
        private void UpdateCollectedLootAmount() =>
            _worldData.LootData.Collect(_loot);

        private void RemoveLootPieceFromSavedPieces()
        {
            LootPieceDataDictionary savedLootPieces = _worldData.LootData.LootPiecesOnScene;

            if (savedLootPieces.Dictionary.ContainsKey(_id)) 
                savedLootPieces.Dictionary.Remove(_id);
        }

        private void UpdateWorldData()
        {
            UpdateCollectedLootAmount();
            RemoveLootPieceFromSavedPieces();
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