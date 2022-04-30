using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public static class HighScore 
{
     public static void Start()
    {
        Assets.GetInstance().Bird.OnDiead += Bird_OnDiead;
    }

    private static void Bird_OnDiead(object sender, System.EventArgs e)
    {
        SethighScore(Assets.GetInstance().level.PlayerScore);
    }

    public static bool SethighScore(int score)
    {
        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt("HighScore", score);
            return true;
        }
        return false;
    }

    public static int GetHighScore()
    {
       return PlayerPrefs.GetInt("HighScore");
    }
}
