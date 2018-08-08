using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitScript {

    [SerializeField]
    int bonusAttack = 3;

    [SerializeField]
    int bonusHP = 5;

    [SerializeField]
    float bonusRange = 0;

    [SerializeField]
    int bonusDefense = 0;

    [SerializeField]
    float bonusSpeed = 0.2f;

    int statMultiplier = 0;

    // Use this for initialization
    void Start () {
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
	void FixedUpdate () {
        currentState.Action();
        if (isAlive == false)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void SetStats(int multiplier)
    {
        statMultiplier = multiplier;
        totalAttack = totalAttack + bonusAttack * statMultiplier;
        totalHP = totalHP + bonusHP * statMultiplier;
        totalRange = totalRange + bonusRange * statMultiplier;
        defense = defense + bonusDefense * statMultiplier;
        totalSpeed = totalSpeed + bonusSpeed * statMultiplier;
    }
}
