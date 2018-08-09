using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Text gameStateText;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    MarbleInput marbleInput;

    [SerializeField]
    MarbleManager marbleManager;

    [SerializeField]
    EnemyManager enemyManager;

    public GameObject ToWorldMap;
    public GameObject ReStart;

    bool playerWin = false;

    bool enemyWin = false;

    public bool PlayerWin
    {
        get
        {
            return playerWin;
        }
    }

    public bool EnemyWin
    {
        get
        {
            return enemyWin;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!playerWin && !enemyWin)
        {
            if (!enemy.GetComponent<BaseScript>().IsAlive)
            {
                playerWin = true;
                marbleInput.AllowInputOff();
                marbleManager.AllowSpawnOff();
                enemyManager.AllowSpawnOff();
                WinState();
            }
            if (!player.GetComponent<BaseScript>().IsAlive)
            {
                enemyWin = true;
                marbleInput.AllowInputOff();
                marbleManager.AllowSpawnOff();
                enemyManager.AllowSpawnOff();
                LoseState();
            }
        }
    }

    void WinState()
    {
        gameStateText.text = "Win";
        gameStateText.enabled = true;
        EndState();
    }

    void LoseState()
    {
        gameStateText.text = "Lose";
        gameStateText.enabled = true;
        EndState();
    }


    void EndState()
    {
        Time.timeScale = 0;
        ReStart.SetActive(true);
        ToWorldMap.SetActive(true);
    }

    public void Re_Start()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
        Time.timeScale = 1;
    }

    public void To_World_Map()
    {
        SceneManager.LoadScene("Level Select");
        Time.timeScale = 1;
    }
}
