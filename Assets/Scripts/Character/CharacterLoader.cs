using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public GameObject tappyPrefab;
    public GameObject bamPrefab;
    public GameObject biggiePrefab;
    public GameObject oguPrefab;

    public Transform characterParent;

    private void Awake()
    {
        CharacterType selected = (CharacterType)PlayerPrefs.GetInt("SelectedCharacter", 0);

        GameObject selectedPrefab = selected switch
        {
            CharacterType.Bam => bamPrefab,
            CharacterType.Biggie => biggiePrefab,
            CharacterType.Ogu => oguPrefab,
            _ => tappyPrefab
        };

        if (selectedPrefab != null)
        {
            GameObject instance = Instantiate(selectedPrefab, characterParent);
            StackManager stack = FindObjectOfType<StackManager>();
            stack.activeCharacter = instance.GetComponent<CharacterPassive>();
        }
        else
        {
            Debug.LogError("Selected character prefab not assigned!");
        }
    }
}