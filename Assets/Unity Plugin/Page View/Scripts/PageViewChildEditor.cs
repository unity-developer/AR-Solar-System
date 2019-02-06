#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace ASPageView
{
    [CustomEditor(typeof(PageViewChild))]
    public class PageViewChildEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PageViewChild pageViewChild = target as PageViewChild;

            PageView pageView = pageViewChild.GetParentPageView();

            if (Selection.activeTransform != null && Selection.activeTransform.GetComponent<PageViewChild>() != null)
            {
                for (int i = 0; i < pageView.GetPageViewChildren().Count; i++)
                {
                    PageViewChild child = pageView.GetPageViewChildren()[i];
                    child.gameObject.SetActive(child == pageViewChild);
                }
            }
        }
    }
}
#endif
