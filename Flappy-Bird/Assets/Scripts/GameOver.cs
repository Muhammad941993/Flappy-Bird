using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Text finalScore;
    private Text finalHighScore;

    // Start is called before the first frame update
    void Start()
    {
        finalScore = GameObject.Find("FinalScore").GetComponent<Text>();
        finalHighScore = GameObject.Find("FinalHighScore").GetComponent<Text>();

        GameObject.Find("MainMenue").GetComponent<Button>().onClick.AddListener(delegate () { SceneLoader.LoadTargetScene(SceneLoader.Scene.MainMenue); });
      
       GameObject.Find("Retry").GetComponent<Button>().onClick.AddListener(delegate () { SceneLoader.LoadTargetScene(SceneLoader.Scene.GameScene); });
      
        HideGameOverMenue();
        Assets.GetInstance().Bird.OnDiead += Bird_OnDieadGameOver;
    }

    private void Bird_OnDieadGameOver(object sender, System.EventArgs e)
    {
        finalScore.text = Assets.GetInstance().level.PlayerScore.ToString();

        if(Assets.GetInstance().level.PlayerScore >= HighScore.GetHighScore())
        {
          
            finalHighScore.text = "NEW HIGHSCORE";
        }
        else
        {
            
            finalHighScore.text = "HIGHSCORE : " + HighScore.GetHighScore();
        }

        ShowGameOverMenue();
    }

    
    

    private void HideGameOverMenue()
    {
        gameObject.SetActive(false);
    }

    private void ShowGameOverMenue()
    {
        gameObject.SetActive(true);
    }
}
