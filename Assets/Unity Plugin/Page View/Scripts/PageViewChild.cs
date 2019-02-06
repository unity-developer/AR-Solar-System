using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASPageView
{
    [ExecuteInEditMode]
    public class PageViewChild : MonoBehaviour
    {
        private PageView _parentPageView;

        void Start()
        {
            _FindParentPageView();
            ResizeToFitParentPageView();
        }

        public PageView GetParentPageView()
        {
            if (_parentPageView == null)
            {
                _FindParentPageView();
            }
            return _parentPageView;
        }

        private void _FindParentPageView()
        {
            Transform child = this.transform;
            while (_parentPageView == null && child.parent != null)
            {
                if (child.transform.parent.GetComponent<PageView>() != null)
                {
                    _parentPageView = child.transform.parent.GetComponent<PageView>();
                }
                else
                {
                    child = child.parent;
                }
            }
        }

        public void ResizeToFitParentPageView()
        {
            if (_parentPageView != null)
            {
                RectTransform rt = this.gameObject.GetComponent<RectTransform>();
                Rect parentViewportRect = _parentPageView.viewport.rect;
                rt.sizeDelta = new Vector2(parentViewportRect.width, parentViewportRect.height);
            }
        }
    }
}
