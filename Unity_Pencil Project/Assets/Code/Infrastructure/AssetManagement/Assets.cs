using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public class Assets : IAssets
    {
        public GameObject Instantiate(string path, Vector3 atPosition)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, atPosition,Quaternion.identity);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}