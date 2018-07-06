using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : UnitState
{

    LayerMask layerMask;

    GameObject target;

    private float timer = 0f;

    private float attackSpeed = 2f;

    float distance = 5f;

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
        timer += Time.deltaTime;
        CheckEnemy();
        if (target != null)
        {
            Attack();
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("attack");
        timer = 0f;
        unitScript.GetComponent<Animator>().SetBool("isAttacking", true);
        unitScript.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void CheckEnemy()
    {
        bool isEmpty = true;
        Vector2 point = unitScript.GetComponent<Transform>().position;
        Collider2D[] colliders = new Collider2D[4];
        colliders = Physics2D.OverlapCircleAll(point, distance, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != unitScript.gameObject)
            {
                isEmpty = false;
                target = colliders[i].gameObject;
                break;
            }
        }
        if (isEmpty)
        {
            unitScript.SetState(new MoveState(unitScript));
        }
    }

    void Attack()
    {
        if (timer >= attackSpeed)
        {
            timer = 0f;
            //GameObject temp = GameObject.Instantiate(target.GetComponent<UnitScript>().Projectile, target.GetComponent<Transform>().GetChild(0).position, Quaternion.identity);
            //temp.transform.SetParent(target.GetComponent<Transform>());
            //temp.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
            target.GetComponent<UnitScript>().RecieveDamage(unitScript.TotalAttack);
        }
    }
}
