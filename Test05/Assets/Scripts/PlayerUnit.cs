using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitScript {

    [SerializeField]
    int bonusAttack = 3;

    [SerializeField]
    int bonusHP = 5;

    [SerializeField]
    float bonusRange = 0;

    [SerializeField]
    float bonusDefense = 0;

    [SerializeField]
    GameObject marbleColor;

    public int[] colors = new int[4];

    // Use this for initialization
    void Awake () {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        attackRange = gameObject.GetComponent<UnitDisplay>().unit.attackRange;
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
        defense = gameObject.GetComponent<UnitDisplay>().unit.defense;
        isAlive = true;
        gameManager = FindObjectOfType<GameManager>();
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
        int fitColor = 0;
        int fitSparkle = 1;
        for (int i = 0; i < 4; i++)
        {
            if (colors[i] == marbleColor.GetComponent<MarbleDisplay>().marble.id)
            {
                fitColor++;
            }
            if (colors[i] == (marbleColor.GetComponent<MarbleDisplay>().marble.id + 3))
            {
                fitSparkle++;
            }
        }
        switch (fitColor)
        {
            case 4:
                totalAttack = totalAttack + bonusAttack * 10 * fitSparkle;
                totalHP = totalHP + bonusHP * 10 * fitSparkle;
                totalRange = totalRange + bonusRange * 10 * fitSparkle;
                defense = defense + bonusDefense * 10 * fitSparkle;
                break;
            case 3:
                totalAttack = totalAttack + bonusAttack * 4 * fitSparkle;
                totalHP = totalHP + bonusHP * 4 * fitSparkle;
                totalRange = totalRange + bonusRange * 4 * fitSparkle;
                defense = defense + bonusDefense * 4 * fitSparkle;
                break;
            case 2:
                totalAttack = totalAttack + bonusAttack * 2 * fitSparkle;
                totalHP = totalHP + bonusHP * 2 * fitSparkle;
                totalRange = totalRange + bonusRange * 2 * fitSparkle;
                defense = defense + bonusDefense * 2 * fitSparkle;
                break;
            case 1:
                totalAttack = totalAttack + bonusAttack * fitSparkle;
                totalHP = totalHP + bonusHP * fitSparkle;
                totalRange = totalRange + bonusRange * fitSparkle;
                defense = defense + bonusDefense * fitSparkle;
                break;
        }
    }
}
