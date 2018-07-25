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

    public int[] colors = new int[4];

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

    public void SetColors(int[] newColors)
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
            switch (colors[i])
            {
                case 0:
                    totalAttack = totalAttack + bonusAttack;
                    break;
                case 1:
                    totalHP = totalHP + bonusHP;
                    break;
                case 2:
                    totalSpeed = totalSpeed + bonusSpeed;
                    break;
                case 3:
                    totalAttack = totalAttack + 2 * bonusAttack;
                    break;
                case 4:
                    totalHP = totalHP + 2 * bonusHP;
                    break;
                case 5:
                    totalSpeed = totalSpeed + 2 * bonusSpeed;
                    break;
            }
        }
    }
}
