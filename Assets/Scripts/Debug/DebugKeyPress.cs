using UnityEngine;

namespace AdventureAssembly.Debug
{
    public abstract class DebugKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;
        [SerializeField] private DebugInputMode _inputMode = DebugInputMode.Press;

        protected virtual void Update()
        {
            if (_key == KeyCode.None)
                return;

            if (_inputMode == DebugInputMode.Press && Input.GetKeyDown(_key))
                DebugKeyPressed();

            if (_inputMode == DebugInputMode.Hold && Input.GetKey(_key))
                DebugKeyPressed();
        }

        public abstract void DebugKeyPressed();
    }
}