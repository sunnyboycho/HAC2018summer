using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleManager : MonoBehaviour {

    [SerializeField]
    Transform[] initialSpawn;

    [SerializeField]
    GameObject[] marbleSpawnPoints = new GameObject[4];

    [SerializeField]
    GameObject[] marbles = new GameObject[3];

    [SerializeField]
    float marbleSpeed = 5;

    [SerializeField]
    float waitSeconds = 2;

    int[] sparkle = { -1, -1, -1, -1 };

    int sparkleNumber = 0;

    int sparkleIndex;

    int[] marbleNumber = { 0, 0, 0, 0 };

    bool isFull = false;

    bool isReady = false;

    bool allowSpawn = true;

    bool isDoubleSpeed = false;

    [SerializeField]
    Transform field;

    // Use this for initialization
    void Start ()
    {
        InitialSpawn();
        sparkleNumber = 0;
        sparkleIndex = -1;
        StartCoroutine("InitialWait");
        //field = gameObject.transform.GetChild(1).transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (allowSpawn)
        {
            if (isReady && !isFull)
            {
                SpawnMarbles();
            }
        }
    }

    public void SwitchAllowSpawn()
    {
        allowSpawn = !allowSpawn;
    }

    void InitialSpawn()
    {
        for (int i = 0; i < initialSpawn.Length; i++)
        {
            marbleNumber[i%4]++;
            int randomMarble = Random.Range(0, 3);
            GameObject temp = Instantiate(marbles[randomMarble], initialSpawn[i].position, Quaternion.identity);
            temp.name = Random.Range(0, 1000).ToString();
            temp.transform.SetParent(field);
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
        sparkleIndex = GetSparkle();
        if (sparkleIndex == -1)
        {
            StartCoroutine("SpawningMarbles");
        }
        else
        {
            StartCoroutine("SpawningSparkle");
        }
    }

    int GetSparkle()
    {
        for (int i = 0; i < sparkle.Length; i++)
        {
            if (sparkle[i] != -1)
            {
                return i;
            }
        }
        return -1;
    }

    IEnumerator SpawningSparkle()
    {
        /*
        int[] tempMarbles = new int[sparkleNumber];
        int tempNumber = sparkleNumber;
        for (int i = 0; i < sparkle.Length; i++)
        {
            if (sparkle[i] != -1)
            {
                tempNumber--;
                tempMarbles[tempNumber] = sparkle[i];
            }
        }
        int[] randomposition = new int[sparkleNumber];
        for (int i = 0; i < sparkleNumber; i++)
        {
            randomposition[i] = Random.Range(0, 3);
            if (i >= 2)
            {
                while (randomposition[i - 1] == randomposition[i])
                {
                    randomposition[i] = Random.Range(0, 3);
                }
            }
        }
        */
        int randomposition = Random.Range(0, 3);
        for (int i = 0; i < 4; i++)
        {
            if (i != randomposition)
            {
                if (marbleNumber[i] < 12)
                {
                    marbleNumber[i]++;
                    int randomMarble = Random.Range(0, 3);
                    GameObject tempUnit = Instantiate(marbles[randomMarble], marbleSpawnPoints[i].transform.position, Quaternion.identity);
                    tempUnit.name = Random.Range(0, 1000).ToString();
                    tempUnit.transform.SetParent(field);
                    tempUnit.GetComponent<Rigidbody2D>().velocity = new Vector2(-marbleSpeed, 0);
                }
            }
            else
            {
                if (marbleNumber[i] < 12)
                {
                    marbleNumber[i]++;
                    int sparkleMarble = 0;
                    if (sparkle[sparkleIndex] < 3)
                    {
                        sparkleMarble = sparkle[sparkleIndex] + 3;
                    }
                    GameObject tempUnit = Instantiate(marbles[sparkleMarble], marbleSpawnPoints[i].transform.position, Quaternion.identity);
                    tempUnit.name = Random.Range(0, 1000).ToString();
                    tempUnit.transform.SetParent(field);
                    tempUnit.GetComponent<Rigidbody2D>().velocity = new Vector2(-marbleSpeed, 0);
                }
            }
        }
        sparkle[sparkleIndex] = -1;
        sparkleIndex = -1;
        yield return new WaitForSeconds(waitSeconds);
        isReady = true;
        CheckifFull();
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

    public void sparkleDibs(int dibs)
    {
        for (int i = 0; i < sparkle.Length; i++)
        {
            if (sparkle[i] == -1)
            {
                sparkle[i] = dibs;
                sparkleNumber++;
                break;
            }
        }
    }

    IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(waitSeconds);
        isReady = true;
    }

    public void DoubleSpeedStart()
    {
        if (!isDoubleSpeed)
        {
            isDoubleSpeed = true;
            waitSeconds = waitSeconds / 2.0f;
        }
    }

    public void DoubleSpeedEnd()
    {
        isDoubleSpeed = false;
        waitSeconds = waitSeconds * 2.0f;
    }
}
