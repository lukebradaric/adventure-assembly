using AdventureAssembly.Core;
using UnityEngine;

namespace AdventureAssembly.Debug
{
    public class DebugSpawnGold : DebugKeyPress
    {
        public override void DebugKeyPressed()
        {
            GoldManager.Instance.AddGold(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}