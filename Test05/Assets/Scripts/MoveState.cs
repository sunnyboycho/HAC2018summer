using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : UnitState
{
    int playerPositive = 1;

    LayerMask layerMask;

    float distance;

    public MoveState(UnitScript unitScript) : base(unitScript)
    {
        if (unitScript.gameObject.CompareTag("Player"))
        {
            playerPositive = 1;
            layerMask = LayerMask.GetMask("Enemy");
        }
        else if (unitScript.gameObject.CompareTag("Enemy"))
        {
            playerPositive = -1;
            layerMask = LayerMask.GetMask("Player");
        }
    }

    public override void Action()
    {
        if (unitScript.IsAlive)
        {
            unitScript.GetComponent<Rigidbody2D>().velocity = new Vector2(playerPositive * unitScript.TotalSpeed, 0);
        }
        CheckEnemy();
    }

    public override void OnStateEnter()
    {
        Debug.Log("move");
        distance = unitScript.AttackRange;
        unitScript.GetComponent<Animator>().SetBool("isAttacking", false);
    }

    void CheckEnemy()
    {
        Vector2 point = unitScript.GetComponent<Transform>().position;
        Collider2D[] colliders = new Collider2D[4];
        colliders = Physics2D.OverlapCircleAll(point, distance, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != unitScript.gameObject)
            {
                unitScript.SetState(new AttackState(unitScript));
                break;
            }
        }
    }
}
