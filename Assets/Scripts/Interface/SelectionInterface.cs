using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class SelectionInterface : Interface
    {
        protected List<SelectionElement> _selectionElements = new List<SelectionElement>();

        protected void OnAddElement(SelectionElement element)
        {
            element.SelectionInterface = this;
            _selectionElements.Add(element);
        }

        protected void RemoveSelectionElement(SelectionElement element)
        {
            if (_selectionElements.Contains(element))
            {
                Debug.LogError($"Selection element was not found in list!");
                return;
            }

            _selectionElements.Remove(element);
        }

        protected void DestroyAllSelectionElements()
        {
            foreach (SelectionElement element in _selectionElements)
            {
                Destroy(element.gameObject);
            }

            _selectionElements.Clear();
        }

        public override void OnShow()
        {
            base.OnShow();

            foreach (SelectionElement element in _selectionElements)
            {
                element.OnShow();
            }
        }

        public override void OnHide()
        {
            base.OnHide();

            foreach (SelectionElement element in _selectionElements)
            {
                element.OnHide();
            }
        }

        public virtual void OnElementSelected(SelectionElement selectionElement) { }
    }
}