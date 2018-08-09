using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : UnitScript
{

    [SerializeField]
    int flatBonusAttack = 3;

    [SerializeField]
    int flatBonusHP = 5;

    [SerializeField]
    float flatBonusRange = 0;

    [SerializeField]
    int flatBonusDefense = 0;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        totalRange = gameObject.GetComponent<UnitDisplay>().unit.attackRange;
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        currentHP = totalHP;
        SetHealthUI();
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
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

    public void SetStats(int multiplier)
    {
        totalAttack = totalAttack + flatBonusAttack;
        totalHP = totalHP + flatBonusHP;
        totalRange = totalRange + flatBonusRange;
        defense = defense + flatBonusDefense;
    }
}
