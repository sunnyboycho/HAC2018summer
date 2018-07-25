using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Character Character;
    
    public void ChangeGameScene()
    {
        string gameScene = Character.CurrentPin.SceneToLoad;
        SceneManager.LoadScene(gameScene);
    }
}
