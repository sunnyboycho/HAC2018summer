﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitScript : MonoBehaviour {

    protected UnitState currentState;

    protected Animator animator;

    protected AudioSource deathAudio;

    protected GameObject target;

    int startingHealth;

    int currentHealth;

    [SerializeField]
    Slider slider;

    [SerializeField]
    Image fillImage;

    [SerializeField]
    TextMeshProUGUI damageText;

    [SerializeField]
    Animator damageIndicator;

    Color fullHealthColor = Color.green;
    Color zeroHealthColor = Color.red;

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

    protected int currentHP;

    public int CurrentHP
    {
        get
        {
            return currentHP;
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

    protected float totalRange;

    public float TotalRange
    {
        get
        {
            return totalRange;
        }
    }

    protected int defense;

    public int Defense
    {
        get
        {
            return defense;
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

    protected GameManager gameManager;

    public GameManager GameManager
    {
        get
        {
            return gameManager;
        }
    }

    // Use this for initialization
    void Start () {
        damageText.text = "";
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
        int dmg = damage - defense;
        Debug.Log("take " + dmg + " damage");
        if (dmg <= 0)
        {
            dmg = 1;
        }
        currentHP -= dmg;
        StartCoroutine("SetHealth");
        CheckHealth();
        damageText.text = dmg.ToString();
        StartCoroutine("ShowDamage");
    }

    void CheckHealth()
    {
        if (currentHP <= 0 && isAlive)
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
        slider.GetComponent<CanvasGroup>().alpha = 0;
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
            GameObject temp = GameObject.Instantiate(projectile, GetComponent<Transform>().GetChild(0).position, Quaternion.identity);
            temp.transform.SetParent(GetComponent<Transform>());
            temp.GetComponent<ProjectileScript>().StartProjectile(target.transform, TotalAttack);
        }
        else
        {
            target.GetComponent<UnitScript>().RecieveDamage(TotalAttack);
        }
    }

    
    protected void SetHealthUI()
    {
        slider.value = (float)currentHP/(float)totalHP;
        slider.GetComponent<CanvasGroup>().alpha = 0;
        //fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHP / totalHP);
    }

    IEnumerator SetHealth()
    {
        slider.GetComponent<CanvasGroup>().alpha = 1;
        slider.value = (float)currentHP / (float)totalHP;
        yield return new WaitForSeconds(0.4f);
        if ((float)currentHP / (float)totalHP > 0.3)
        {
            slider.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    IEnumerator ShowDamage()
    {
        Debug.Log("Show damage " + damageText.text);
        damageIndicator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(1f);
        damageIndicator.SetBool("isDamaged", false);
        damageText.text = "";
    }
}
