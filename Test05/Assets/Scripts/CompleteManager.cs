using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteManager : MonoBehaviour {

    public GameObject box3;

    private Queue<string> Sentences;

	// Use this for initialization
	void Start () {
        box3.SetActive(false);
    }

    public void StartFinishSentence() //need to trigger this when after the unit spawn
    {
        box3.SetActive(true); //ERROR?????
        Time.timeScale = 0f;
    }

    public void CompleteTutorial()
    {
        box3.SetActive(false);
        Time.timeScale = 1f;
    }
}
