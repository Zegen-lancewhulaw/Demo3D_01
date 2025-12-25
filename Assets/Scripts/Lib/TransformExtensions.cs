using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void SortChildren(this Transform transform)
    {
        if (transform == null) return;
        if (transform.childCount > 0)
        {
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < transform.childCount; ++i)
            {
                children.Add(transform.GetChild(i));
            }
            children.Sort((a, b) => (a.name.Length.CompareTo(b.name.Length)));
            for (int i = 0; i < children.Count; ++i)
            {
                if (i != children[i].GetSiblingIndex())
                {
                    children[i].SetSiblingIndex(0);
                }
            }
        }
    }

    public static Transform DeepFind(this Transform transform, string name)
    {
        if (transform == null || name == null) return null;
        if (transform.childCount == 0) return null;
        Transform child = null;
        if ((child = transform.Find(name)) != null)
        {
            return child;
        }
        else
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                child = DeepFind(transform.GetChild(i), name);
                if (child != null) return child;
            }
            return null;
        }
    }
}
