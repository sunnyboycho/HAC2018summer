using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    int level;

    [SerializeField]
    float spawnSpeed = 5.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine("Create");
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator Create()
    {
        for (int i = 0; i < level; i++)
        {
            gameObject.GetComponent<UnitCreator>().CreateEnemyUnit();
            yield return new WaitForSeconds(spawnSpeed);
        }
    }
}
