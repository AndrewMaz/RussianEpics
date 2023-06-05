using System;
using UnityEngine;

namespace Biofeedback
{
    public sealed class SafeAreaCanvas : MonoBehaviour
    {
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable IDE0044 // Add readonly modifier

        private enum ScreenType { Rect }

        [SerializeField] private ScreenType screenType = ScreenType.Rect;

        [Space()]

        [SerializeField] private bool _setLeft = true;
        [SerializeField] private bool _setRight = true;
        [SerializeField] private bool _setTop = false;
        [SerializeField] private bool _setDown = false;

        [Space()]

        [SerializeField] private bool _needManualUpdate = false;
        [SerializeField, Range(0f, 1f)] private float _manualOffset = 0;

        private Rect _safeArea;
        private ScreenOrientation _orientation;

#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members


        private void Start()
        {
            _orientation = Screen.orientation;
            Setup();
        }

#if UNITY_EDITOR
        public void NeedManualUpdate()
        {
            _needManualUpdate = true;
        }

        private void FixedUpdate()
        {
            if (_orientation != Screen.orientation || _safeArea != Screen.safeArea || _needManualUpdate)
            {
                _orientation = Screen.orientation;
                _safeArea = Screen.safeArea;
                _needManualUpdate = false;
                Setup();
            }
        }
#endif

        private void Setup()
        {
#if UNITY_IPHONE || UNITY_ANDROID
            Rect safeArea = Screen.safeArea;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 manualOffset = Vector2.one * _manualOffset;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = anchorMin + safeArea.size;
            
            anchorMin /= screenSize;
            anchorMax /= screenSize;

            anchorMin += manualOffset;
            anchorMax -= manualOffset;

            if (!_setLeft)
            {
                anchorMin.x = 0f;
            }
            if (!_setTop)
            {
                anchorMin.y = 0f;
            }
            if (!_setRight)
            {
                anchorMax.x = 1f;
            }
            if (!_setDown)
            {
                anchorMax.y = 1f;
            }

            switch (screenType)
            {
                case ScreenType.Rect:
                    RectTransform rectForSet = GetComponent<RectTransform>();
                    if (rectForSet != null)
                    {
                        SetAnchors(rectForSet, anchorMin, anchorMax);

                        Debug.Log($"[SafeAreaCanvas][Setup] set rect size {rectForSet.rect.size}");
                    }
                    else
                    {
                        Debug.Log($"[SafeAreaCanvas][Setup] rect notFound");
                    }
                    break;
            }        
#else
            // hide warnings
            _ = _setLeft;
            _ = _setTop;
            _ = _setRight;
            _ = _setDown;
            _ = _needManualUpdate;
            _ = _manualOffset;
            _ = screenType;
            _ = _safeArea;
            _ = _orientation;
#endif
        }

        private void SetAnchors(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.ForceUpdateRectTransforms();

            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.ForceUpdateRectTransforms();
        }  
    }
}