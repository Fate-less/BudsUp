using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewardManager : MonoBehaviour
{
    public Button claimButton;
    public Image claimImage;
    public Sprite claimSprite;
    public Sprite claimedSprite;
    public int dailyRewardAmount = 100;

    private const string LastClaimKey = "LastDailyClaimDate";
    private const string CoinKey = "TotalCoins";

    private void Start()
    {
        if (IsRewardAvailableToday())
        {
            claimButton.interactable = true;
            claimImage.sprite = claimSprite;
            claimButton.onClick.AddListener(ClaimDailyReward);
        }
        else
        {
            claimButton.interactable = false;
            claimImage.sprite = claimedSprite;
        }
    }

    private bool IsRewardAvailableToday()
    {
        string lastClaimDate = PlayerPrefs.GetString(LastClaimKey, "");
        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
        return lastClaimDate != today;
    }

    public void ClaimDailyReward()
    {
        int currentCoins = PlayerPrefs.GetInt(CoinKey, 0);
        currentCoins += dailyRewardAmount;
        PlayerPrefs.SetInt(CoinKey, currentCoins);

        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
        PlayerPrefs.SetString(LastClaimKey, today);
        PlayerPrefs.Save();

        claimImage.sprite = claimedSprite;
        claimButton.interactable = false;
    }
}