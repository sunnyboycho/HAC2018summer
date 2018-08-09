using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject Resume;
    public GameObject Restart;
    public GameObject ToWorldMap;
    public GameObject QuitGame;

    private GameObject Level_Pin;

    void Awake()
    {
        Level_Pin = Pin.Level_Pin;
    }

    public void Pause_Game()
    {
        Time.timeScale = 0;
        Resume.SetActive(true);
        Restart.SetActive(true);
        ToWorldMap.SetActive(true);
        QuitGame.SetActive(true);
    }

    public void Resume_Game()
    {
        Resume.SetActive(false);
        Restart.SetActive(false);
        ToWorldMap.SetActive(false);
        QuitGame.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart_Game()
    {
        string CurrentScene = Character.CurrentPin.SceneToLoad;
        SceneManager.LoadScene(CurrentScene);
        Time.timeScale = 1;
    }

    public void To_World_Map()
    {
        SceneManager.LoadScene("Level Select");
        Time.timeScale = 1;
    }

    public void Quit_Game()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
