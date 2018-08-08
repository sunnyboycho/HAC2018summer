using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    int waveNumber = 0;

    [SerializeField]
    GameObject[] enemyTypes;

    [SerializeField]
    GameObject[] enemyArray;

    [SerializeField]
    int enemyNumber;

    [SerializeField]
    float spawnInterval = 5.0f;

    [SerializeField]
    float intervalWait = 5.0f;

    [SerializeField]
    int perWave = 5;

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
        if (allowSpawn)
        {
            yield return new WaitForSeconds(intervalWait);
            waveNumber++;
            for (int i = 0; i < enemyNumber; i++)
            {
                for (int j = 0; j < enemyArray.Length; j++)
                {
                    if (enemyArray[j] != null)
                    {
                        GameObject temp = gameObject.GetComponent<UnitCreator>().CreateEnemyUnit(dict[enemyArray[j]]);
                        temp.GetComponent<EnemyUnit>().SetStats(waveNumber/perWave);
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
}
