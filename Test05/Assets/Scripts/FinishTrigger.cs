using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour {

    public GameObject UnitCheck;

    CompleteManager completemanager = new CompleteManager();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (UnitCheck != null)
        {
           completemanager.StartFinishSentence();
        }
	}
}
