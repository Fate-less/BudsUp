using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MenuPanelResizer : MonoBehaviour
{
    public int numberOfTabs = 6;

    void Start()
    {
        ResizePanel();
    }

    public void ResizePanel()
    {
        RectTransform rt = GetComponent<RectTransform>();

        float scaleFactor = GetComponentInParent<Canvas>().scaleFactor;

        float canvasWidth = Screen.width / scaleFactor;

        rt.sizeDelta = new Vector2(canvasWidth * numberOfTabs, rt.sizeDelta.y);
    }
}
