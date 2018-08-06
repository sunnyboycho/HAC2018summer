using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Tutorial {

    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
