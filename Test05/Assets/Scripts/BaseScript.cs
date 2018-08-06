﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : UnitScript{

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        totalAttack = gameObject.GetComponent<UnitDisplay>().unit.attack;
        totalRange = gameObject.GetComponent<UnitDisplay>().unit.attackRange;
        totalHP = gameObject.GetComponent<UnitDisplay>().unit.hp;
        currentHP = totalHP;
        SetHealthUI();
        totalSpeed = gameObject.GetComponent<UnitDisplay>().unit.speed;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
