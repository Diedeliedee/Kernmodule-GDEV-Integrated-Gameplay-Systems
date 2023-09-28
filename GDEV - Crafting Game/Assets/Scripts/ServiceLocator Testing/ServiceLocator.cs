using UnityEngine;

namespace Testing
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance { get; private set; }

        public IInventory Inventory => inventory;
        public ITickManager TickManager => tickManager;

        private IInventory inventory;
        private ITickManager tickManager;

        public ServiceLocator()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Other instance of a service locator already exists.");
                return;
            }
            Instance = this;

            SetupServices();
        }

        private void SetupServices()
        {
            tickManager = new TickManager();
        }
    }
}
