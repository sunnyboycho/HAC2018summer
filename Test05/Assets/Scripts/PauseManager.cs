using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject Resume;
    public GameObject ToWorldMap;
    public GameObject QuitGame;
    public GameObject ReStart;

    public void Pause_Game()
    {
        Time.timeScale = 0;
        Resume.SetActive(true);
        ReStart.SetActive(true);
        ToWorldMap.SetActive(true);
        QuitGame.SetActive(true);
    }

    public void Resume_Game()
    {
        Resume.SetActive(false);
        ReStart.SetActive(false);
        ToWorldMap.SetActive(false);
        QuitGame.SetActive(false);
        Time.timeScale = 1;
    }

    public void Re_Start()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(CurrentScene);
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
    }
}
