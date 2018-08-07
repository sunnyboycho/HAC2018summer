using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour {

    [SerializeField]
    Text text;
    
    MarbleManager marbleManager;

    bool statUp = false;

    private void Start()
    {
        marbleManager = FindObjectOfType<MarbleManager>();
        text.text = "";
    }

    public void StatUp()
    {
        if (!statUp)
        {
            statUp = true;
            text.text = "S1 Active";
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
        text.text = "S2 Active";
        marbleManager.DoubleSpeedStart();
        StartCoroutine("DoubleSpeedWait");
    }

    IEnumerator DoubleSpeedWait()
    {
        yield return new WaitForSeconds(20f);
        marbleManager.DoubleSpeedEnd();
        text.text = "";
    }
}
