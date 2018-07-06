using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnits : UnitScript {

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        animator.SetBool("isAlive", true);
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        Debug.Log("attack" + gameObject.GetComponent<UnitDisplay>().unit.attack + totalAttack);
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
        SetState(new MoveState(this));
    }
	
	// Update is called once per frame
	void Update () {
        currentState.Action();
    }
}
