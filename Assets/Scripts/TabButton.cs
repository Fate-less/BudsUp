using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    public TabType tabToOpen;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            TabManager.Instance.GoToTab(tabToOpen);
        });
    }
}