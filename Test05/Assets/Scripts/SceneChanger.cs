using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Character Character;
    private GameObject Level_Pin;

    void Awake()
    {
        Level_Pin = Pin.Level_Pin;
    }

    public void ChangeGameScene()
    {
        MapManager.CurrentPosition = Character.CurrentPin;
        MapManager.IsStartPositionChanged = 1;

        Level_Pin = GameObject.Find("Level_Pin");
        Level_Pin.SetActive(false);
        Pin.IsPinExist = 1;

        string gameScene = Character.CurrentPin.SceneToLoad;
        SceneManager.LoadScene(gameScene);
    }
}
