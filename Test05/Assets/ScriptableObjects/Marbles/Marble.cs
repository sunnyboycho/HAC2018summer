using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Marble", menuName = "Marble")]
public class Marble : ScriptableObject
{

    public string color;

    public int id;

    public Sprite image;
}
