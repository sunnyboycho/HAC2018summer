using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour {

    [SerializeField]
    Text text;

    MarbleManager marbleManager;

    [SerializeField]
    Text[] timerTexts = new Text[2];

    [SerializeField]
    float[] cooldowns = new float[2];

    float[] timers = new float[2];

    bool[] isReady = new bool[2];

    bool statUp = false;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            timers[i] = 0;
            isReady[i] = false;
        }
        marbleManager = FindObjectOfType<MarbleManager>();
        text.text = "";
    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (!isReady[i])
            {
                timers[i] = timers[i] + Time.deltaTime;
                timerTexts[i].text = ((int)timers[i]).ToString();
                if (timers[i] >= cooldowns[i])
                {
                    timers[i] = 0;
                    isReady[i] = true;
                }
            }
            else
            {
                timerTexts[i].text = "Ready";
            }
        }
    }

    public void StatUp()
    {
        if (isReady[1])
        {
            statUp = true;
            isReady[1] = false;
            text.text = "S2 Active";
            StartCoroutine("StatUpWait");
        }
    }

    public bool GetStatUp()
    {
        return statUp;
    }

    IEnumerator StatUpWait()
    {
        yield return new WaitForSeconds(20f);
        statUp = false;
        text.text = "";
    }

    public void MarbleSpeedUp()
    {
        if (isReady[0])
        {
            isReady[0] = false;
            text.text = "S1 Active";
            marbleManager.DoubleSpeedStart();
            StartCoroutine("DoubleSpeedWait");
        }
    }

    IEnumerator DoubleSpeedWait()
    {
        yield return new WaitForSeconds(20f);
        marbleManager.DoubleSpeedEnd();
        text.text = "";
    }
}
