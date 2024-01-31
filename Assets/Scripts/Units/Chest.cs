using AdventureAssembly.Core;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Chest : MonoBehaviour
    {
        public VoidScriptableEvent _chestOpenScriptableEvent;

        private void OnEnable()
        {
            GridManager.AddPosition(new Vector2Int((int)transform.position.x, (int)transform.position.y), gameObject);
        }

        private void OnDisable()
        {
            GridManager.RemovePosition(new Vector2Int((int)transform.position.x, (int)transform.position.y), gameObject);
        }

        public void OnCollect()
        {
            _chestOpenScriptableEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}