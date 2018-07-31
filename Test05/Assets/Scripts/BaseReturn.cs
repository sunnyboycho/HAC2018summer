using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseReturn : MonoBehaviour {

    [SerializeField]
    Transform[] playerSpawn = new Transform[5];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        int spawnPoint = Random.Range(0, playerSpawn.Length);
        //if(other.gameObject.tag == "Player")
        //{
            Debug.Log("collision deteced");
            other.transform.position = playerSpawn[spawnPoint].position;
        //}
    }
}
