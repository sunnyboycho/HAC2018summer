using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    
    protected UnitState currentState;

    protected Animator animator;

    protected AudioSource deathAudio;

    protected GameObject target;

    [SerializeField]
    protected bool isProjectileUnit;

    public bool IsProjectileUnit
    {
        get
        {
            return isProjectileUnit;
        }
    }

    [SerializeField]
    protected GameObject projectile;

    public GameObject Projectile
    {
        get
        {
            return projectile;
        }
    }

    protected float attackRange;

    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }

    protected int totalAttack;

    public int TotalAttack
    {
        get
        {
            return totalAttack;
        }
    }

    protected int totalHP;

    public int TotalHP
    {
        get
        {
            return totalHP;
        }
    }

    protected float totalSpeed;

    public float TotalSpeed
    {
        get
        {
            return totalSpeed;
        }
    }

    protected bool isAlive = false;

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }

	// Use this for initialization
	void Start () {
        /*
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        animator.SetBool("isAlive", true);
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        Debug.Log("attack" + gameObject.GetComponent<UnitDisplay>().unit.attack + totalAttack);
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
        SetState(new MoveState(this));
        */
    }
	
	// Update is called once per frame
	void Update () {
        //currentState.Action();
    }

    public void SetState(UnitState unitState)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = unitState;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public void RecieveDamage(int damage)
    {
        Debug.Log("take " + damage + " damage");
        totalHP -= damage;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (totalHP <= 0 && isAlive)
        {
            gameObject.layer = 11;
            gameObject.tag = "Dead";
            gameObject.GetComponent<Collider2D>().enabled = false;
            deathAudio = gameObject.GetComponent<AudioSource>();
            isAlive = false;
            animator.SetTrigger("isDead");
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f);
        deathAudio.Play();
        //yield return new WaitForSeconds(3.5f);
        //Destroy(gameObject);
    }

    public void DeadAndDelete()
    {
        Destroy(gameObject);
    }

    public void AquireTarget(GameObject tar)
    {
        target = tar;
    }

    public void Attack()
    {
        if (IsProjectileUnit)
        {
            GameObject temp = GameObject.Instantiate(target.GetComponent<UnitScript>().Projectile, GetComponent<Transform>().GetChild(0).position, Quaternion.identity);
            temp.transform.SetParent(GetComponent<Transform>());
            temp.GetComponent<ProjectileScript>().StartProjectile(target.transform, TotalAttack);
        }
        else
        {
            target.GetComponent<UnitScript>().RecieveDamage(TotalAttack);
        }
    }
}
