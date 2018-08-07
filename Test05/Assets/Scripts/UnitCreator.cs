using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreator : MonoBehaviour {

    [SerializeField]
    SpriteRenderer[] unitPortrait = new SpriteRenderer[3];

    [SerializeField]
    Sprite[] unitImageAlt = new Sprite[3];

    [SerializeField]
    Transform[] parent = new Transform[2];
    
    [SerializeField]
    GameObject[] unit = new GameObject[3];

    [SerializeField]
    GameObject[] altUnit = new GameObject[3];

    [SerializeField]
    GameObject[] enemyUnit;

    [SerializeField]
    Transform[] playerSpawn = new Transform[5];

    [SerializeField]
    Transform[] enemySpawn = new Transform[5];

    private void Start()
    {

    }

    public void CreateUnit(int type, int[] colors)
    {
        int spawnPoint = Random.Range(0,playerSpawn.Length);
        GameObject newUnit = Instantiate(unit[type], playerSpawn[spawnPoint].position, Quaternion.identity);
        newUnit.GetComponent<PlayerUnit>().SetColors(colors);
        newUnit.transform.SetParent(parent[0]);
        newUnit.GetComponent<MeshRenderer>().sortingOrder = spawnPoint;
    }

    public void CreateEnemyUnit(int i)
    {
        int enemySpawnPoint = Random.Range(0, enemySpawn.Length);
        GameObject newUnit = Instantiate(enemyUnit[i], enemySpawn[enemySpawnPoint].position, Quaternion.identity);
        newUnit.transform.SetParent(parent[1]);
        newUnit.GetComponent<MeshRenderer>().sortingOrder = enemySpawnPoint;
    }

    public void SwapUnits()
    {
        GameObject temp;
        for (int i = 0; i < 3; i++)
        {
            temp = unit[i];
            unit[i] = altUnit[i];
            altUnit[i] = temp;
        }
        Sprite tempSprite;
        for (int i = 0; i < 3; i++)
        {
            tempSprite = unitPortrait[i].sprite;
            unitPortrait[i].sprite = unitImageAlt[i];
            unitImageAlt[i] = tempSprite;
        }
    }
}
