using Code.UI.Services.Factory;

namespace Code.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uIFactory)
        {
            _uiFactory = uIFactory;
        }
        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
            }
            
        }
    }
}