using System.Collections.Generic;
using UnityEngine;

public class TalentTreeCalculater : MonoBehaviour
{
    [SerializeField] private TalentTree talentTree;
    [SerializeField] private float space;
    [SerializeField] GameObject nodePrefab;

    private int rootNodeId;
    private List<RectTransform> rootChilds;

    [ContextMenu("Organize")]
    private void Organize()
    {
        ClearChilds();



        //if (talentTree == null || nodePrefab == null) return;
        //
        //foreach (var node in talentTree.Nodes)
        //{
        //    GameObject instance = Instantiate(nodePrefab, this.transform);
        //    RectTransform rect = instance.GetComponent<RectTransform>();
        //
        //    if (node.RootNode)
        //    {
        //        rootNodeId = node.GetID();
        //        rect.anchoredPosition = Vector2.zero;
        //    }
        //    else
        //    {
        //        if (rootNodeId == node.GetParentID())
        //        {
        //            rootChilds.Add(rect);
        //
        //            float angleStep = 360f / rootChilds.Count;
        //
        //            for (int i = 0; i < rootChilds.Count; i++)
        //            {
        //                float angle = 45f + i * angleStep;
        //
        //                Vector2 position = GetChildsPosition(angle);
        //
        //                rootChilds[i].anchoredPosition = position;
        //            }
        //
        //        }
        //    }
        //}
    }

    private Vector2 GetChildsPosition(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * space;
    }

    private void ClearChilds()
    {
        rootChilds.Clear();
        for (int i = transform.childCount -1; i >=0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
