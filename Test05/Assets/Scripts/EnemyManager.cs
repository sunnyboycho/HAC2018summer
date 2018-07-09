using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    int enemyNumber;

    [SerializeField]
    float spawnInterval = 5.0f;

    [SerializeField]
    float initialWait = 5.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine("Create");
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator Create()
    {
        yield return new WaitForSeconds(initialWait); ;
        for (int i = 0; i < enemyNumber; i++)
        {
            gameObject.GetComponent<UnitCreator>().CreateEnemyUnit();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
