using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsManager : MonoBehaviour {

    [SerializeField]
    MarbleInput marbleInput;

    [SerializeField]
    MarbleManager marbleManager;

    [SerializeField]
    EnemyManager enemyManager;

    [SerializeField]
    GameObject[] illustrations;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PauseGame()
    {
        marbleInput.AllowInputOff();
        marbleManager.AllowSpawnOff();
        enemyManager.AllowSpawnOff();
    }

    void ResumeGame()
    {
        marbleInput.AllowInputOn();
        marbleManager.AllowSpawnOn();
        enemyManager.AllowSpawnOn();
    }
}
