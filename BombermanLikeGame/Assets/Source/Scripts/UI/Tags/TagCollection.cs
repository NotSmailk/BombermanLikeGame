using System.Collections.Generic;
using UnityEngine;

public class TagCollection
{
    public List<RectTransform> panels = new List<RectTransform>();

    public void ClearAll()
    {
        foreach (var i in panels)
        {
            Object.Destroy(i.gameObject);
        }

        panels.Clear();
    }

    public void Remove(RectTransform panel)
    {
        if (panels.Remove(panel))
        {
            panels.Remove(panel);
            Object.Destroy(panel.gameObject);
        }
    }
}
