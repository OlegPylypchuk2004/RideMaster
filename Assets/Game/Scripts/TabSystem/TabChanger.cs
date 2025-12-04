using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

namespace TabSystem
{
    public class TabChanger : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _initialTabIndex;
        [SerializeField] private Tab[] _tabs;
        [SerializeField] private TabButton[] _tabButtons;
        [SerializeField] private RectTransform _tabsParentRectTransform;
        [SerializeField] private CanvasGroup _tabsParentCanvasGroup;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private Ease _ease;

        private int _activeTabIndex;
        private Sequence _currentSequence;

        private void Awake()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            _initialTabIndex = Mathf.Clamp(_initialTabIndex, 0, _tabs.Length - 1);
            _activeTabIndex = _initialTabIndex;
            _tabsParentRectTransform.anchoredPosition = new Vector3(screenSize.x * _activeTabIndex * -1f, 0f, 0f);

            for (int i = 0; i < _tabs.Length; i++)
            {
                _tabs[i].ApplySize(screenSize.x, screenSize.y);

                if (i == _activeTabIndex)
                {
                    _tabs[i].Activate();
                }
                else
                {
                    _tabs[i].Deactivate();
                }
            }
        }

        private void OnEnable()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                tabButton.Selected += OnTabButtonSelected;
            }
        }

        private void OnDisable()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                tabButton.Selected -= OnTabButtonSelected;
            }
        }

        private void OnTabButtonSelected(Tab tab)
        {
            if (tab == null || !_tabs.Contains(tab) || _tabs[_activeTabIndex] == tab)
            {
                return;
            }

            _activeTabIndex = Array.IndexOf(_tabs, tab);
            _tabsParentCanvasGroup.interactable = false;

            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                for (int i = 0; i < _tabs.Length; i++)
                {
                    _tabs[i].Activate();
                }
            });

            _currentSequence.Append(_tabsParentRectTransform.DOMoveX(Screen.width * _activeTabIndex * -1f, _duration)
                .SetEase(_ease));

            _currentSequence.AppendCallback(() =>
            {
                for (int i = 0; i < _tabs.Length; i++)
                {
                    if (i == _activeTabIndex)
                    {
                        continue;
                    }

                    _tabs[i].Deactivate();
                }

                _tabsParentCanvasGroup.interactable = true;
            });
        }
    }
}