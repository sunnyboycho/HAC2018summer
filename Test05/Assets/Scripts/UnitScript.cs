using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {
    
    protected UnitState currentState;

    protected Animator animator;

    protected int totalAttack;

    protected AudioSource deathAudio;

    [SerializeField]
    protected GameObject projectile;

    public GameObject Projectile
    {
        get
        {
            return projectile;
        }
    }

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

    protected int totalSpeed;

    public int TotalSpeed
    {
        get
        {
            return totalSpeed;
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
        if (totalHP <= 0)
        {
            deathAudio = gameObject.GetComponent<AudioSource>();
            animator.SetBool("isAlive", false);
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.0f);
        deathAudio.Play();
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
