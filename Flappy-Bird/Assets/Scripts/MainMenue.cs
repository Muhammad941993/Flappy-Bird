using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class MainMenue : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(delegate () { SceneLoader.LoadTargetScene(SceneLoader.Scene.GameScene); });
       GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(delegate () { Application.Quit(); });
    }

   
    

}
