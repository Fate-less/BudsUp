using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabAutoSizer : MonoBehaviour
{
    LayoutElement layoutElement;
    RectTransform tabRect;
    static Vector2 lastScreenSize;

    void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        tabRect = GetComponent<RectTransform>();
        Resize();
    }

    void Update()
    {
        if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
        {
            Resize();
        }
    }

    void Resize()
    {
        lastScreenSize = new Vector2(Screen.width, Screen.height);

        float scaleFactor = GetComponentInParent<Canvas>().scaleFactor;
        float canvasWidth = Screen.width / scaleFactor;
        float canvasHeight = Screen.height / scaleFactor;

        layoutElement.preferredWidth = canvasWidth;
        layoutElement.preferredHeight = canvasHeight;
    }
}