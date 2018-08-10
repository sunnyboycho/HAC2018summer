using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour {

    public int progress;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        SaveManager.SaveProgress(this);
    }

    public void Load()
    {
        int loadedProgress = SaveManager.LoadProgress();

        progress = loadedProgress;
    }
}
