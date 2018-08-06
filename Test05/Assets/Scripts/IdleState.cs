using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : UnitState
{

    LayerMask layerMask;

    public IdleState(UnitScript unitScript) : base(unitScript)
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
        
    }

    public override void OnStateEnter()
    {
        Debug.Log("attack");
        //timer = 0f;
        unitScript.GetComponent<Animator>().SetBool("isAttacking", false);
        unitScript.GetComponent<Animator>().SetBool("isIdle", true);
        unitScript.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
