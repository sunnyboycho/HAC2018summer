using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{

    public string unitName;

    public Sprite image;

    public int hp;

    public int attack;

    public int speed;
}