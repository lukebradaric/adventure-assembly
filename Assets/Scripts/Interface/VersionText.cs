using TMPro;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class VersionText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _text.text = Application.version;
        }
    }
}