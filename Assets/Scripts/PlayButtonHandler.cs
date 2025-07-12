using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public void OnPlayPressed()
    {
        int characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);

        string sceneName = characterIndex switch
        {
            0 => "Gameplay_Tappy",
            1 => "Gameplay_Bam",
            2 => "Gameplay_Biggie",
            3 => "Gameplay_Ogu",
            _ => "Gameplay_Tappy"
        };

        SceneManager.LoadScene(sceneName);
    }
}