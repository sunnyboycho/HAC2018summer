using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Character Character;

    public GameObject Level_Pin;

    public void ChangeGameScene()
    {
        MapManager.CurrentPosition = Character.CurrentPin;
        MapManager.IsStartPositionChanged = 1;

        Level_Pin.SetActive(false);

        string gameScene = Character.CurrentPin.SceneToLoad;
        SceneManager.LoadScene(gameScene);
    }
}
