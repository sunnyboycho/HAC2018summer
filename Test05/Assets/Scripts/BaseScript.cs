using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour {

    protected Animator animator;

    protected AudioSource deathAudio;
    
    protected int totalHP = 50;

    public int TotalHP
    {
        get
        {
            return totalHP;
        }
    }

    protected bool isAlive = true;

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
    }

    // Update is called once per frame
    void Update()
    {

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
            gameObject.GetComponent<Collider2D>().enabled = false;
            deathAudio = gameObject.GetComponent<AudioSource>();
            animator.SetTrigger("isDead");
            isAlive = false;
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f);
        deathAudio.Play();
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}
