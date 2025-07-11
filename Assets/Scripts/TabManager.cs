using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public static TabManager Instance { get; private set; }

    [Header("References")]
    public RectTransform menuPanel;
    public float transitionTime = 0.5f;
    public float tabWidth = 1080f;

    private Dictionary<TabType, int> tabIndexMap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        tabIndexMap = new Dictionary<TabType, int>
        {
            { TabType.Home, 0 },
            { TabType.Character, 1 },
            { TabType.Building, 2 },
            { TabType.Daily, 3 },
            { TabType.Leaderboard, 4 },
            { TabType.Options, 5 }
        };
    }

    public void GoToTab(TabType type)
    {
        if (!tabIndexMap.ContainsKey(type))
        {
            Debug.LogWarning("Unknown tab type: " + type);
            return;
        }

        int tabIndex = tabIndexMap[type];
        Vector2 targetPos = new Vector2(-tabIndex * tabWidth, 0);

        LeanTween.move(menuPanel, targetPos, transitionTime)
                 .setEase(LeanTweenType.easeInOutCubic);
    }

    public void GoBack()
    {
        GoToTab(TabType.Home);
    }

}

public enum TabType
{
    Home,
    Character,
    Building,
    Daily,
    Leaderboard,
    Options
}
