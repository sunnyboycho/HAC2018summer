using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Portrait", menuName = "Portrait")]
public class Portrait : ScriptableObject {

    public string characterName;

    public Sprite face;
}
