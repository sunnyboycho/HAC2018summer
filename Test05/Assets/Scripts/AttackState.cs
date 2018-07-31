using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : UnitState
{

    LayerMask layerMask;

    GameObject target;
    
    private float attackSpeed;

    float distance;

    public AttackState(UnitScript unitScript) : base(unitScript)
    {
        if (unitScript.gameObject.CompareTag("Player"))
        {
            layerMask = LayerMask.GetMask("Enemy");
        }
        else if (unitScript.gameObject.CompareTag("Enemy"))
        {
            layerMask = LayerMask.GetMask("Player");
        }
    }

    public override void Action()
    {
        CheckEnemy();
        CheckWin();
        if (target != null)
        {
            PassTarget();
            //Attack();
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("attack");
        distance = unitScript.AttackRange;
        unitScript.GetComponent<Animator>().SetBool("isAttacking", true);
        unitScript.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void CheckEnemy()
    {
        bool isEmpty = true;
        Vector2 point = unitScript.GetComponent<Transform>().position;
        Collider2D[] colliders = new Collider2D[4];
        colliders = Physics2D.OverlapBoxAll(point, new Vector2(distance, 5f), 0f, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != unitScript.gameObject)
            {
                isEmpty = false;
                target = colliders[i].gameObject;
                break;
            }
        }
        if (isEmpty && unitScript.IsAlive)
        {
            unitScript.SetState(new MoveState(unitScript));
        }
    }

    void PassTarget()
    {
        unitScript.AquireTarget(target);
    }

    void CheckWin()
    {
        if (unitScript.GameManager.EnemyWin || unitScript.GameManager.PlayerWin)
        {
            unitScript.SetState(new IdleState(unitScript));
        }
    }
}
