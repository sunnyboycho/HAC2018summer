using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleManager : MonoBehaviour {

    [SerializeField]
    GameObject[] marbleSpawnPoints = new GameObject[4];

    [SerializeField]
    GameObject[] marbles = new GameObject[3];

    [SerializeField]
    float marbleSpeed = 5;

    [SerializeField]
    float waitSeconds = 2;

    int[] marbleNumber = { 0, 0, 0, 0 };

    bool isFull = false;

    bool isReady = true;

    [SerializeField]
    Transform field;

    // Use this for initialization
    void Start () {
        //field = gameObject.transform.GetChild(1).transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (isReady && !isFull)
        {
            SpawnMarbles();
        }
    }

    IEnumerator SpawningMarbles()
    {
        for (int i = 0; i < 4; i++)
        {
            if(marbleNumber[i] < 12)
            {
                marbleNumber[i]++;
                int randomMarble = Random.Range(0, 3);
                GameObject temp = Instantiate(marbles[randomMarble], marbleSpawnPoints[i].transform.position, Quaternion.identity);
                temp.name = Random.Range(0, 1000).ToString();
                temp.transform.SetParent(field);
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-marbleSpeed, 0);
            }
        }
        yield return new WaitForSeconds(waitSeconds);
        isReady = true;
        CheckifFull();
    }

    void SpawnMarbles()
    {
        isReady = false;
        StartCoroutine("SpawningMarbles");
    }

    public void CheckifFull()
    {
        if ((marbleNumber[0] >= 12) && (marbleNumber[1] >= 12) && (marbleNumber[2] >= 12) && (marbleNumber[3] >= 12))
        {
            isFull = true;
        }
        isFull = false; ;
    }

    public void decreaseMarble(int row)
    {
        marbleNumber[row]--;
    }
}
