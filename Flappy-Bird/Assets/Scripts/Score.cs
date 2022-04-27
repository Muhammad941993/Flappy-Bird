using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text score;
    private Level level;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        level = GameObject.Find("Level").GetComponent<Level>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

   public void UpdateScoreText()
    {
       score.text = level.PlayerScore.ToString();
    }
}
