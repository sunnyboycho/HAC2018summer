using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitScript {

    SkillManager skillMangager;

    [SerializeField]
    int bonusAttack = 3;

    [SerializeField]
    int bonusHP = 5;

    [SerializeField]
    float bonusRange = 0;

    [SerializeField]
    int bonusDefense = 0;

    [SerializeField]
    float bonusSpeed = 0.2f;

    [SerializeField]
    GameObject marbleColor;

    float statMultiplier = 1f;

    public int[] colors = new int[4];

    // Use this for initialization
    void Awake () {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        totalRange = gameObject.GetComponent<UnitDisplay>().unit.attackRange;
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
        defense = gameObject.GetComponent<UnitDisplay>().unit.defense;
        isAlive = true;
        gameManager = FindObjectOfType<GameManager>();
        skillMangager = FindObjectOfType<SkillManager>();
        SetState(new MoveState(this));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    currentState.Action();
        if (isAlive == false)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void SetColors(int[] newColors)
    {
        for (int i = 0; i < 4; i++)
        {
            colors[i] = newColors[i];
        }
        SetStats();
        currentHP = totalHP;
        SetHealthUI();
        GetComponentInChildren<MarbleSubscript>().ReceiveColors(colors);
    }

    public void SetStats()
    {
        int fitColor = 0;
        int fitSparkle = 1;
        for (int i = 0; i < 4; i++)
        {
            if (colors[i] == marbleColor.GetComponent<MarbleDisplay>().marble.id)
            {
                fitColor++;
            }
            if (colors[i] == (marbleColor.GetComponent<MarbleDisplay>().marble.id + 3))
            {
                fitColor++;
                fitSparkle++;
            }
        }
        if (skillMangager.GetStatUp())
        {
            statMultiplier = 2f;
        }
        else
        {
            statMultiplier = 1f;
        }
        switch (fitColor)
        {
            case 4:
                totalAttack = totalAttack + bonusAttack * 10 * fitSparkle;
                totalHP = totalHP + bonusHP * 10 * fitSparkle;
                totalRange = totalRange + bonusRange * 10 * fitSparkle;
                defense = defense + bonusDefense * 10 * fitSparkle;
                totalSpeed = totalSpeed + bonusSpeed * 10 * fitSparkle;
                break;
            case 3:
                totalAttack = totalAttack + bonusAttack * 4 * fitSparkle;
                totalHP = totalHP + bonusHP * 4 * fitSparkle;
                totalRange = totalRange + bonusRange * 4 * fitSparkle;
                defense = defense + bonusDefense * 4 * fitSparkle;
                totalSpeed = totalSpeed + bonusSpeed * 10 * fitSparkle;
                break;
            case 2:
                totalAttack = totalAttack + bonusAttack * 2 * fitSparkle;
                totalHP = totalHP + bonusHP * 2 * fitSparkle;
                totalRange = totalRange + bonusRange * 2 * fitSparkle;
                defense = defense + bonusDefense * 2 * fitSparkle;
                totalSpeed = totalSpeed + bonusSpeed * 10 * fitSparkle;
                break;
            case 1:
                totalAttack = totalAttack + bonusAttack * fitSparkle;
                totalHP = totalHP + bonusHP * fitSparkle;
                totalRange = totalRange + bonusRange * fitSparkle;
                defense = defense + bonusDefense * fitSparkle;
                totalSpeed = totalSpeed + bonusSpeed * 10 * fitSparkle;
                break;
        }
        totalAttack = totalAttack * (int)statMultiplier;
        totalHP = totalHP * (int)statMultiplier;
        totalRange = totalRange * (int)statMultiplier;
        defense = defense * (int)statMultiplier;
        totalSpeed = totalSpeed * (int)statMultiplier;
    }
}
