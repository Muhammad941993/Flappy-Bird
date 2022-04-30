using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text score;
    private Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        highScore = GameObject.Find("HighScore").GetComponent<Text>();
        SetHighScore();
        HighScore.Start();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SetHighScore()
    {
        highScore.text = "HighScore: " + HighScore.GetHighScore();
    }
   public void UpdateScoreText()
    {
        score.text = Assets.GetInstance().level.PlayerScore.ToString();
    }
}
