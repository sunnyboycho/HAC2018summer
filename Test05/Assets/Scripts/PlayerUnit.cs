using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitScript {

    [SerializeField]
    int bonusAttack = 3;

    [SerializeField]
    int bonusHP = 5;

    [SerializeField]
    float bonusSpeed = 1;

    public string[] colors = new string[4];

    // Use this for initialization
    void Awake () {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        attackRange = gameObject.GetComponent<UnitDisplay>().unit.attackRange;
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
        isAlive = true;
        SetState(new MoveState(this));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    currentState.Action();
        if (isAlive == false)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void SetColors(string[] newColors)
    {
        for (int i = 0; i < 4; i++)
        {
            colors[i] = newColors[i];
        }
        SetStats();
        currentHP = totalHP;
        SetHealthUI();
        GetComponentInChildren<MarbleSubscript>().ReceiveColors(colors);
    }

    public void SetStats()
    {
        for (int i = 0; i < 4; i++)
        {
            if (colors[i].Equals("red"))
            {
                totalAttack = totalAttack + bonusAttack;
            }
            else if (colors[i].Equals("green"))
            {
                totalHP = totalHP + bonusHP;
            }
            else if (colors[i].Equals("blue"))
            {
                totalSpeed = totalSpeed + bonusSpeed;
            }
            else
            {
                Debug.Log("Stat allocation error no color input");
            }
        }
    }
}
