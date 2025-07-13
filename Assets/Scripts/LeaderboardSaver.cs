using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeaderboardSaver
{
    public static void TrySaveScore(int newScore)
    {
        int[] scores = new int[3];
        for (int i = 0; i < 3; i++)
            scores[i] = PlayerPrefs.GetInt($"LeaderboardScore{i}", 0);

        for (int i = 0; i < scores.Length; i++)
        {
            if (newScore > scores[i])
            {
                for (int j = scores.Length - 1; j > i; j--)
                    scores[j] = scores[j - 1];

                scores[i] = newScore;
                break;
            }
        }

        for (int i = 0; i < scores.Length; i++)
            PlayerPrefs.SetInt($"LeaderboardScore{i}", scores[i]);

        PlayerPrefs.Save();
    }
}