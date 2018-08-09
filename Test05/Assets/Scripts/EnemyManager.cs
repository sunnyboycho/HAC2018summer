using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    int waveCount = 0;

    [SerializeField]
    GameObject[] enemyTypes;

    [SerializeField]
    List<GameObject> enemyList;

    [SerializeField]
    int enemyNumber;

    [SerializeField]
    float spawnInterval = 5.0f;

    [SerializeField]
    float intervalWait = 5.0f;

    [SerializeField]
    int perWave = 5;

    [SerializeField]
    bool enableWaveIncrease = false;

    [SerializeField]
    bool enableStatIncrease = false;

    bool allowSpawn = true;

    Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>();

    // Use this for initialization
    void Start () {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            dict.Add(enemyTypes[i], i);
        }
        StartCoroutine("Create");
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator Create()
    {
        int typeCount = 0;
        if (allowSpawn)
        {
            yield return new WaitForSeconds(intervalWait);
            for (int i = 0; i < enemyNumber; i++)
            {
                waveCount++;
                if (enableWaveIncrease && (waveCount % perWave) == 0)
                {
                    enemyList.Add(enemyTypes[typeCount]);
                    typeCount = ++typeCount % enemyTypes.Length;
                }
                for (int j = 0; j < enemyList.ToArray().Length; j++)
                {
                    if (enemyList.ToArray()[j] != null)
                    {
                        GameObject temp = gameObject.GetComponent<UnitCreator>().CreateEnemyUnit(dict[enemyList.ToArray()[j]]);
                        if (enableStatIncrease)
                        {
                            temp.GetComponent<EnemyUnit>().SetStats(waveCount / perWave);
                        }
                        else
                        {
                            temp.GetComponent<EnemyUnit>().SetStats(0);
                        }
                    }
                    yield return new WaitForSeconds(spawnInterval);
                }
                yield return new WaitForSeconds(intervalWait);
            }
        }
    }

    public void SwitchAllowSpawn()
    {
        allowSpawn = !allowSpawn;
    }

    public void AllowSpawnOff()
    {
        allowSpawn = false;
    }

    public void AllowSpawnOn()
    {
        allowSpawn = false;
    }
}
