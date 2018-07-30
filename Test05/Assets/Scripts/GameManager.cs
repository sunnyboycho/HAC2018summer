using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Text gameStateText;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject enemy;

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
        if (!enemy.GetComponent<BaseScript>().IsAlive)
        {
            playerWin = true;
            WinState();
        }
        if (!player.GetComponent<BaseScript>().IsAlive)
        {
            enemyWin = true;
            LoseState();
        }
    }

    void WinState()
    {
        gameStateText.text = "Win";
        gameStateText.enabled = true;
    }

    void LoseState()
    {
        gameStateText.text = "Lose";
        gameStateText.enabled = true;
    }
}
