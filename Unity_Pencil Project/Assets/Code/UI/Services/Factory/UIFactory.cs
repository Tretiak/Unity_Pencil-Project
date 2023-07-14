using Code.Infrastructure.AssetManagement;
using Code.StaticData;
using Code.StaticData.ScriptableObjects.WindowsStaticData;
using Code.UI.Services.Windows;
using UnityEngine;

namespace Code.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            Object.Instantiate(config.Prefab,_uiRoot);
        }

        public void CreateUIRoot()
        {
           _uiRoot = _assets.Instantiate(AssetPath.UIUiRootPath).transform;
        }
    }
}