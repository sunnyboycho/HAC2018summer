using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    public GameObject checkBoxupdate;
    public GameObject checkBoxupdate2;

    public static bool IsTutorialOn = true;


    // Update is called once per frame
    void Update () {

        if (IsTutorialOn == false)
        {
            Resume();
        }
        else
        {
            Pause();
        }

        if (GameObject.Find("checkBoxupdate") != null)
        {
            IsTutorialOn = false;
        }

    }

    void Pause()
    {
        Time.timeScale = 0f;
    }

    void Resume()
    {
        Time.timeScale = 1f;
    }
}
