using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ASPageView
{
    [ExecuteInEditMode, Serializable]
    public class PageView : ScrollRect, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private List<PageViewChild> _pageViewChildren = new List<PageViewChild>();
        private int _currentPageIndex = 0;
        private float _pageWidth = 0f;
        private bool _animate = false;
        private float _lastDragSpeed = 0;
        private Vector2 _destination = Vector2.zero;
        private int _oldChildCount = 0;

        [SerializeField]
        public RectTransform pageIndicator;
        public static int _Currentpage;
        public void Update()
        {
            _Currentpage = _currentPageIndex;
            if (_animate)
            {
                this.content.anchoredPosition = Vector2.Lerp(this.content.anchoredPosition, _destination, 7 * Time.deltaTime);
                if (Mathf.Abs(this.content.anchoredPosition.x - _destination.x) < 0.05f)
                {
                    _animate = false;
                }
            }
            if (_oldChildCount != content.childCount)
            {
                _oldChildCount = content.childCount;
                _PreparePageViewChildren();
                _UpdatePageIndicator();
            }
        }

        private void _PreparePageViewChildren()
        {
            _pageViewChildren.Clear();
            for (int i = 0; i < content.transform.childCount; i++)
            {
                PageViewChild child;
                if ((child = content.transform.GetChild(i).gameObject.GetComponent<PageViewChild>()) != null)
                {
                    _pageViewChildren.Add(child);
                    child.ResizeToFitParentPageView();
                    child.gameObject.SetActive(true);
                }
            }
        }

        private void _UpdatePageIndicator()
        {
            if (pageIndicator.childCount == 0)
            {
                for (int i = 0; i < _pageViewChildren.Count; i++)
                {
                    if (i == _currentPageIndex)
                    {
                        _InstantiatePageIndicatorItem("PageIndicatorFullCircle");
                    }
                    else
                    {
                        _InstantiatePageIndicatorItem("PageIndicatorEmptyCircle");
                    }
                }
            }
            else
            {
                if (pageIndicator.transform.Find("PageIndicatorFullCircle") == null)
                {
                    _InstantiatePageIndicatorItem("PageIndicatorFullCircle");
                }
                while (pageIndicator.transform.childCount > _pageViewChildren.Count && pageIndicator.transform.Find("PageIndicatorEmptyCircle") != null)
                {
                    GameObject.DestroyImmediate(pageIndicator.transform.Find("PageIndicatorEmptyCircle").gameObject);
                }
                while (pageIndicator.transform.childCount < _pageViewChildren.Count)
                {
                    _InstantiatePageIndicatorItem("PageIndicatorEmptyCircle");
                }
                pageIndicator.transform.Find("PageIndicatorFullCircle").SetSiblingIndex(_currentPageIndex);
            }
        }

        private void _InstantiatePageIndicatorItem(string name)
        {
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Page View/Prefabs/" + name)) as GameObject;
            obj.name = name;
            obj.transform.SetParent(pageIndicator.transform, false);
        }

        public List<PageViewChild> GetPageViewChildren()
        {
            return _pageViewChildren;
        }

        public void SetCurrentPage(int index)
        {
            index = Mathf.Min(_pageViewChildren.Count - 1, Mathf.Max(index, 0));
            _destination = this.content.anchoredPosition;
            _destination.x = index * _pageWidth * -1;
            _currentPageIndex = index;
            _UpdatePageIndicator();
            _animate = true;
        }

        public void PreviousPage()
        {
            SetCurrentPage(_currentPageIndex - 1);
        }

        public void NextPage()
        {
            SetCurrentPage(_currentPageIndex + 1);
        }

        public override void OnEndDrag(PointerEventData data)
        {
            base.OnEndDrag(data);
            Vector2 oldDestination = new Vector2(_destination.x, _destination.y);
            _destination = this.content.anchoredPosition;
            _destination.x = Mathf.Min(Mathf.Max((Mathf.Round((_destination.x + _lastDragSpeed * 30) / _pageWidth) * _pageWidth), -1 * (_pageViewChildren.Count - 1) * _pageWidth), 0f);
            _destination.x = Mathf.Max(oldDestination.x - _pageWidth, Mathf.Min(oldDestination.x + _pageWidth, _destination.x));

            _currentPageIndex = (int)Mathf.Abs(Mathf.Round(_destination.x / _pageWidth));
            _UpdatePageIndicator();
            _animate = true;
        }

        public override void OnDrag(PointerEventData data)
        {
            base.OnDrag(data);
            _lastDragSpeed = data.delta.x;
        }

        public override void OnBeginDrag(PointerEventData data)
        {
            base.OnBeginDrag(data);
            _animate = false;
        }

        protected override void Awake()
        {
            base.Awake();
            _destination = this.content.anchoredPosition;
            _PreparePageViewChildren();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            _PreparePageViewChildren();
            _pageWidth = GetComponent<RectTransform>().rect.width;
            _animate = true;
        }
    }
}

