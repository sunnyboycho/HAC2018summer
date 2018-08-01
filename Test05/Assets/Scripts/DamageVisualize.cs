using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageVisualize : MonoBehaviour {
    
    public TextMeshProUGUI damageText;
    
    [SerializeField]
    private Animator damageTextMove;


	// Use this for initialization
    void Start()
    {
        damageText.text = "";
        //damageText.text.alpha = 0;
        damageTextMove.SetBool("isDamaged", false);
    }

    public void TriggerTrue()
    {
        Debug.Log("Show damage " + damageText.text);
        damageTextMove.SetBool("isDamaged", true);
    }

    public void TriggerFalse()
    {
        damageTextMove.SetBool("isDamaged", false);
        damageText.text = "";
    }
}
