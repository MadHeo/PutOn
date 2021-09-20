using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableViewCell<T> : ViewController
{
    public virtual void UpdateContent(T itemData)
    {

    }

    public int DataIndex { get; set; }

    public float Height
    {
        get { return CachedRectTransform.sizeDelta.y; }
        set
        {
            Vector2 sizeDelta = CachedRectTransform.sizeDelta;
            sizeDelta.y = value;
            CachedRectTransform.sizeDelta = sizeDelta;
        }
    }

    public Vector2 Top
    {
        get
        {
            Vector3[] corners = new Vector3[4];
            CachedRectTransform.GetLocalCorners(corners);
            return CachedRectTransform.anchoredPosition +
                new Vector2(0.0f, corners[1].y);
        }
        set
        {
            Vector3[] corners = new Vector3[4];
            CachedRectTransform.GetLocalCorners(corners);
            CachedRectTransform.anchoredPosition =
                value - new Vector2(0.0f, corners[1].y);
        }
    }
}
