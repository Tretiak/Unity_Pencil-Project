using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.ScriptableObjects.WindowsStaticData
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/Window")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}