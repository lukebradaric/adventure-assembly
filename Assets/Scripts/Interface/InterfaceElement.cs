using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace AdventureAssembly.Interface
{
    public abstract class InterfaceElement : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public virtual void OnPointerDown(PointerEventData eventData) { }
        public virtual void OnPointerEnter(PointerEventData eventData) { }
        public virtual void OnPointerExit(PointerEventData eventData) { }

        public virtual void OnShow() { }
        public virtual void OnHide() { }
    }
}