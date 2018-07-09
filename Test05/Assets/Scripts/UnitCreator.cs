using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator : MonoBehaviour {

    [SerializeField]
    Transform[] parent = new Transform[2];
    
    [SerializeField]
    GameObject[] unit;

    [SerializeField]
    GameObject[] enemyUnit;

    [SerializeField]
    Transform playerSpawn;

    [SerializeField]
    Transform enemySpawn;

    [SerializeField]
    Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>();

    private void Start()
    {
        //CreateEnemyUnit();
        for (int i = 0; i < 1; i++)
        {
            dict.Add("SquareTile", unit[i]);
        }
    }

    public void CreateUnit(string type, string[] colors)
    {
        Debug.Log("Create Unit " + type);
        GameObject newUnit = Instantiate(dict[type], playerSpawn.position, Quaternion.identity);
        newUnit.GetComponent<PlayerUnits>().SetColors(colors);
        newUnit.transform.SetParent(parent[0]);
    }

    public void CreateEnemyUnit()
    {
        Debug.Log("Create enemy");
        GameObject newUnit = Instantiate(enemyUnit[0], enemySpawn.position, Quaternion.identity);
        newUnit.transform.SetParent(parent[1]);
    }
}
