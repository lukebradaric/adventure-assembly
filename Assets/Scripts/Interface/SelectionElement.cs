using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureAssembly.Interface
{
    public class SelectionElement : InterfaceElement
    {
        [BoxGroup("Selection Element")]
        [Tooltip("Should this element change scale on hover?")]
        [OdinSerialize] public bool TweenOnHover { get; protected set; } = false;

        [BoxGroup("Selection Element")]
        [ShowIf(nameof(TweenOnHover))]
        [Tooltip("What scale should this element change to when hovered?")]
        [OdinSerialize] public float TweenScale { get; protected set; } = 1f;

        [BoxGroup("Selection Element")]
        [ShowIf(nameof(TweenOnHover))]
        [Tooltip("How long should the tween to scale this element take?")]
        [OdinSerialize] public float TweenDuration { get; protected set; } = 0.25f;

        public bool Interactable { get; set; } = true;
        public SelectionInterface SelectionInterface { get; set; } = null;

        public override void OnPointerDown(PointerEventData eventData)
        {
            // If this element cannot be interacted with, return
            if (Interactable == false)
            {
                return;
            }

            base.OnPointerDown(eventData);
            SelectionInterface?.OnElementSelected(this);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (Interactable == false)
            {
                return;
            }

            base.OnPointerEnter(eventData);
            transform.DOScale(TweenScale, TweenDuration).SetUpdate(true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transform.DOScale(1, TweenDuration).SetUpdate(true);
        }
    }
}