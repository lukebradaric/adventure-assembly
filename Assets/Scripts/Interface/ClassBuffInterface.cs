using AdventureAssembly.Units.Classes;
using DG.Tweening;
using System.Collections.Generic;
using TinyTools.ScriptableVariables;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class ClassBuffInterface : MonoBehaviour
    {
        [Space]
        [Header("Prefabs")]
        [SerializeField] private ClassBuffElement _classBuffElementPrefab;

        [Space]
        [Header("Components")]
        [SerializeField] private RectTransform _verticalLayoutTransform;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Space]
        [Header("Settings")]
        [SerializeField] private FloatScriptableVariable _tweenDuration;

        // Dictionary of each classbuffelement for each active classdata
        private Dictionary<ClassData, ClassBuffElement> _classBuffElements = new Dictionary<ClassData, ClassBuffElement>();

        private void OnEnable()
        {
            ClassManager.ClassDataAdded += OnClassDataAdded;
            ClassManager.ClassCountChanged += OnClassCountChanged;
            ClassManager.ClassDataRemoved += OnClassDataRemoved;
        }

        private void OnDisable()
        {
            ClassManager.ClassDataAdded -= OnClassDataAdded;
            ClassManager.ClassCountChanged -= OnClassCountChanged;
            ClassManager.ClassDataRemoved -= OnClassDataRemoved;
        }

        public void Show()
        {
            _canvasGroup.DOFade(1f, _tweenDuration.Value).SetUpdate(true);
            foreach (var element in _classBuffElements.Values)
            {
                element.Show();
            }
        }

        public void Hide()
        {
            _canvasGroup.DOFade(0f, _tweenDuration.Value).SetUpdate(true);
            foreach (var element in _classBuffElements.Values)
            {
                element.Hide();
            }
        }

        private void OnClassDataAdded(ClassData classData)
        {
            // Instantiate new class buff interface element
            ClassBuffElement classBuffElement = Instantiate(_classBuffElementPrefab, _verticalLayoutTransform);
            classBuffElement.ClassData = classData;

            _classBuffElements.Add(classData, classBuffElement);
        }

        private void OnClassCountChanged(ClassData classData, int count)
        {
            // Update the current count of a class buff interface element
            _classBuffElements[classData].SetCountText(ClassManager.Instance.GetClassCount(classData).ToString());
        }

        private void OnClassDataRemoved(ClassData classData)
        {
            // Remove a class buff interface element
            Destroy(_classBuffElements[classData].gameObject);
            _classBuffElements.Remove(classData);
        }
    }
}