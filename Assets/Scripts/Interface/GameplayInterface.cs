using AdventureAssembly.Core;
using DG.Tweening;
using System.Collections.Generic;
using TinyTools.Generics;
using TinyTools.ScriptableVariables;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class GameplayInterface : Singleton<GameplayInterface>
    {
        [Space]
        [Header("Components")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private List<Interface> _interfaces;

        [Space]
        [Header("Settings")]
        [SerializeField] private FloatScriptableVariable _interfaceTweenDuration;

        public void Show()
        {
            TimeManager.Pause();

            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1f, _interfaceTweenDuration.Value).SetUpdate(true);

            foreach (Interface iface in _interfaces)
            {
                iface.OnShow();
            }
        }

        public void Hide()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0f, _interfaceTweenDuration.Value).SetUpdate(true).OnComplete(() =>
            {
                TimeManager.Unpause();
            });

            foreach (Interface iface in _interfaces)
            {
                iface.OnHide();
            }
        }
    }
}