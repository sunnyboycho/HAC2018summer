using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : UnitState
{

    LayerMask layerMask;

    GameObject target;

    //private float timer = 0f;
    
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
        //timer += Time.deltaTime;
        CheckEnemy();
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
        attackSpeed = unitScript.GetComponent<UnitDisplay>().unit.attackSpeed;
        //timer = 0f;
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
    
    /*
    public void Attack()
    {
        if (unitScript.IsAlive)
        {
            //timer = 0f;
            if (unitScript.IsProjectileUnit)
            {
                GameObject temp = GameObject.Instantiate(unitScript.Projectile, unitScript.GetComponent<Transform>().GetChild(0).position, Quaternion.identity);
                temp.transform.SetParent(unitScript.GetComponent<Transform>());
                temp.GetComponent<ProjectileScript>().StartProjectile(target.transform, unitScript.TotalAttack);
            }
            else
            {
                target.GetComponent<UnitScript>().RecieveDamage(unitScript.TotalAttack);
            }
        }
    }
    */

    void PassTarget()
    {
        unitScript.AquireTarget(target);
    }
}
