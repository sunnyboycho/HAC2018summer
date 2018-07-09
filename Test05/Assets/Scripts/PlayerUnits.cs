using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnits : UnitScript {

    public string[] colors = new string[4];

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        attackRange = gameObject.GetComponent<UnitDisplay>().unit.attackRange;
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
        SetState(new MoveState(this));
    }
	
	// Update is called once per frame
	void Update () {
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
    }

    public void SetStats()
    {
        for (int i = 0; i < 4; i++)
        {
            if (colors[i].Equals("red"))
            {
                totalAttack += 5;
            }
            else if (colors[i].Equals("green"))
            {
                totalHP += 5;
            }
            else if (colors[i].Equals("blue"))
            {
                totalSpeed += 2;
            }
            else
            {
                //Debug.Log("Stat allocation error no color input");
            }
        }
    }
}
