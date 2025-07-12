using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CharacterType
{
    Tappy,
    Bam,
    Biggie,
    Ogu
}

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public Sprite portrait;
    public Sprite pasport;
    public Sprite[] upgradeStageIcons;
}

public class CharacterSelectManager : MonoBehaviour
{
    [Header("UI References")]
    public Image portraitDisplay;
    public Image pasportImage;
    public Image upgradeStageIcon;

    [Header("Characters")]
    public CharacterData[] characters;

    private int currentIndex = 0;

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        UpdateCharacterDisplay();
    }

    public void OnNextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Length;
        UpdateCharacterDisplay();
    }

    public void OnPreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        CharacterData current = characters[currentIndex];

        portraitDisplay.sprite = current.portrait;
        pasportImage.sprite = current.pasport;

        int upgradeLevel = PlayerPrefs.GetInt($"{current.characterName}_UpgradeLevel", 0);
        upgradeLevel = Mathf.Clamp(upgradeLevel, 0, current.upgradeStageIcons.Length - 1);

        upgradeStageIcon.sprite = current.upgradeStageIcons[upgradeLevel];

        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.Save();
    }

    public void UpgradeCharacter()
    {
        string name = characters[currentIndex].characterName;
        int currentLevel = PlayerPrefs.GetInt($"{name}_UpgradeLevel", 0);
        if (currentLevel < 2)
        {
            currentLevel++;
            PlayerPrefs.SetInt($"{name}_UpgradeLevel", currentLevel);
            PlayerPrefs.Save();
            UpdateCharacterDisplay();
        }
    }
}
