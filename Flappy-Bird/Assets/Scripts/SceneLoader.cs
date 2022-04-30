using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class SceneLoader 
{

   public enum Scene
    {
        MainMenue,
        GameScene
    }



    static public void LoadTargetScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
