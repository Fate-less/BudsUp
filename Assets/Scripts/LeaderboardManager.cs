using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardTexts;

    private void Start()
    {
        UpdateLeaderboardUI();
    }

    public void UpdateLeaderboardUI()
    {
        for (int i = 0; i < leaderboardTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt($"LeaderboardScore{i}", 0);
            leaderboardTexts[i].text = $"{i + 1}. {score}";
        }
    }
}