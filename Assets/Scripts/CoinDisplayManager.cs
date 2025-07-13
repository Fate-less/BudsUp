using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplayManager : MonoBehaviour
{
    [Tooltip("All TMPs that should reflect the player's coin count")]
    public List<TextMeshProUGUI> coinTexts = new List<TextMeshProUGUI>();

    private const string CoinKey = "TotalCoins";
    public static CoinDisplayManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateCoinDisplay();
    }

    public void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt(CoinKey, 0);
        foreach (var tmp in coinTexts)
        {
            if (tmp != null)
                tmp.text = totalCoins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        int current = PlayerPrefs.GetInt(CoinKey, 0);
        current += amount;
        PlayerPrefs.SetInt(CoinKey, current);
        PlayerPrefs.Save();
        UpdateCoinDisplay();
    }

    public bool TrySpendCoins(int amount)
    {
        int current = PlayerPrefs.GetInt(CoinKey, 0);
        if (current >= amount)
        {
            current -= amount;
            PlayerPrefs.SetInt(CoinKey, current);
            PlayerPrefs.Save();
            UpdateCoinDisplay();
            return true;
        }

        return false;
    }

    public void RegisterCoinDisplay(TextMeshProUGUI tmp)
    {
        if (!coinTexts.Contains(tmp))
        {
            coinTexts.Add(tmp);
            tmp.text = PlayerPrefs.GetInt(CoinKey, 0).ToString();
        }
    }
}
