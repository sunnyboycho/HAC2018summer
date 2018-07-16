using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleInput : MonoBehaviour {

    Transform[] hits = new Transform[5];

    bool hitsFull = false;

    [SerializeField]
    GameObject[] box;

    bool[,] matrix =
        {
            {false, false, false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false, false, false },
        };

    bool[,] inputMatrix = 
    {
        {false, false, false },
        {false, false, false },
        {false, false, false },
    };

    int type;

    [SerializeField]
    UnitCreator unitCreator;

    [SerializeField]
    MarbleManager marbleManager;

    string[] colors = new string[4];

    int fieldLayerMask;

    int marbleLayerMask;
    
    // Use this for initialization
    void Start () {
        fieldLayerMask = LayerMask.GetMask("Field");
        marbleLayerMask = LayerMask.GetMask("Marble");
        Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("click ");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f, fieldLayerMask);

            if (hit.collider != null)
            {
                //Debug.Log("hit " + hit.transform.name);
                hit.transform.GetComponent<SpriteRenderer>().enabled = true;
                int i = 0;
                while (hits[i] != null && !hits[i].Equals(hit.transform))
                {
                    i++;
                }
                hits[i] = hit.transform;
                if (isHitsFull())
                {
                    hits[0].GetComponent<SpriteRenderer>().enabled = false;
                    hits[1].GetComponent<SpriteRenderer>().enabled = false;
                    hits[2].GetComponent<SpriteRenderer>().enabled = false;
                    hits[3].GetComponent<SpriteRenderer>().enabled = false;
                    hits[4].GetComponent<SpriteRenderer>().enabled = false;
                    hits[0] = null;
                    hits[1] = null;
                    hits[2] = null;
                    hits[3] = null;
                    hits[4] = null;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetMatrix();
            InputMatrix();
            CompareMatrixes();
            Debug.Log("type " + type);
            int n = 0;
            for (int i = 0; i < 5; i++)
            {
                if (hits[i] != null)
                {
                    n++;
                    hits[i].GetComponent<SpriteRenderer>().enabled = false;
                    //hits[i] = null;
                }
            }
            Debug.Log("n " + n);
            if (n == 4)
            {
                if (type < 4)
                {
                    int j = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (hits[i] != null)
                        {
                            Collider2D[] marbleBuffer = new Collider2D[2];
                            marbleBuffer = Physics2D.OverlapBoxAll(hits[i].position, new Vector2(0.8f, 0.8f), 0, marbleLayerMask);
                            if (marbleBuffer[0] != null)
                            {
                                Destroy(marbleBuffer[0].gameObject);
                                marbleManager.decreaseMarble((int)char.GetNumericValue(hits[i].name[0]) - 1);
                                for (int k = 0; k < 4; k++)
                                {
                                    if (colors[k] == "none")
                                    {
                                        j++;
                                        colors[k] = marbleBuffer[0].gameObject.GetComponent<MarbleDisplay>().marble.color;
                                        Debug.Log(colors[k]);
                                        break;
                                    }
                                }
                                marbleManager.CheckifFull();
                            }
                        }
                    }
                    if (j == 4)
                    {
                        unitCreator.CreateUnit(type, colors);
                        Initialize();
                    }
                }
            }
            SetMatrix();
        }
	}

    void SetMatrix()
    {
        int n = 0;
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                matrix[j, i] = box[n].GetComponent<SpriteRenderer>().enabled;
                n++;
            }
        }
    }

    void InputMatrix()
    {
        int[] startPosition = {13, 13 };

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (matrix[i, j])
                {
                    if (startPosition[1] == 13)
                    {
                        startPosition[0] = i;
                        startPosition[1] = j;
                        inputMatrix[0, 0] = true;
                    }
                    else
                    {
                        int jTemp = j - startPosition[1];
                        while (jTemp <= -1)
                        {
                            inputMatrix[0, 2] = inputMatrix[0, 1];
                            inputMatrix[1, 2] = inputMatrix[1, 1];
                            inputMatrix[2, 2] = inputMatrix[2, 1];
                            inputMatrix[0, 1] = inputMatrix[0, 0];
                            inputMatrix[1, 1] = inputMatrix[1, 0];
                            inputMatrix[2, 1] = inputMatrix[2, 0];
                            inputMatrix[0, 0] = false;
                            inputMatrix[1, 0] = false;
                            inputMatrix[2, 0] = false;
                            jTemp++;
                            startPosition[1] = startPosition[1] - 1;
                        }
                        inputMatrix[i - startPosition[0], j - startPosition[1]] = true;
                    }
                }
            }
        }
       /*
        int jMin = Mathf.Clamp(startPosition[1] - 2, 0, 12);
        int jMax = Mathf.Clamp(startPosition[1] + 2, 0, 12);

        for (int i = startPosition[0]; i < 4; i++)
        {
            for (int j = jMin; j < jMax; j++)
            {
                if (matrix[i, j])
                {
                    int jTemp = j - startPosition[1];
                    while (jTemp < 0)
                    {
                        inputMatrix[0, 2] = inputMatrix[0, 1];
                        inputMatrix[1, 2] = inputMatrix[1, 1];
                        inputMatrix[2, 2] = inputMatrix[2, 1];
                        inputMatrix[0, 1] = inputMatrix[0, 0];
                        inputMatrix[1, 1] = inputMatrix[1, 0];
                        inputMatrix[2, 1] = inputMatrix[2, 0];
                        inputMatrix[0, 0] = false;
                        inputMatrix[1, 0] = false;
                        inputMatrix[2, 0] = false;
                        jTemp++;
                        startPosition[1] = startPosition[1] - 1;
                    }
                    inputMatrix[i - startPosition[0], j - startPosition[1]] = true;
                }
            }
        }
        */
    }

    void CompareMatrixes()
    {
        bool[] compare = new bool[13] {true, true, true, true, true, true, true, true, true, true, true, true, true};
        int count = 0;
        int num = 14;
        if (inputMatrix[0,0])
        {
            compare[2] = false;
            compare[4] = false;
            compare[6] = false;
            compare[12] = false;
        }
        if (inputMatrix[1, 0])
        {
            compare[3] = false;
            compare[6] = false;
            compare[8] = false;
            compare[11] = false;
        }
        if (inputMatrix[0, 1])
        {
            compare[1] = false;
            compare[5] = false;
            compare[10] = false;
            compare[12] = false;
        }
        if (inputMatrix[1, 1])
        {
            compare[5] = false;
            compare[7] = false;
            compare[9] = false;
            compare[11] = false;
        }
        if (inputMatrix[2, 0])
        {
            compare[0] = false;
            compare[1] = false;
            compare[3] = false;
            compare[4] = false;
            compare[8] = false;
            compare[9] = false;
            compare[10] = false;
            compare[11] = false;
            compare[12] = false;
        }
        if (inputMatrix[2, 1])
        {
            compare[0] = false;
            compare[2] = false;
            compare[3] = false;
            compare[4] = false;
            compare[7] = false;
            compare[9] = false;
            compare[10] = false;
            compare[11] = false;
            compare[12] = false;
        }
        if (inputMatrix[0, 2])
        {
            compare[0] = false;
            compare[1] = false;
            compare[2] = false;
            compare[3] = false;
            compare[5] = false;
            compare[6] = false;
            compare[7] = false;
            compare[8] = false;
            compare[10] = false;
        }
        if (inputMatrix[1, 2])
        {
            compare[0] = false;
            compare[1] = false;
            compare[2] = false;
            compare[4] = false;
            compare[5] = false;
            compare[6] = false;
            compare[7] = false;
            compare[8] = false;
            compare[9] = false;
        }
        for (int i = 0; i < 13; i++)
        {
            if (compare[i])
            {
                //Debug.Log("true for " + i);
                count++;
                num = i;
            }
        }
        if (count != 1)
        {
            num = 14;
        }

        switch (num)
        {
            case 0:
                type = 0;
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                type = 1;
                break;
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                type = 2;
                break;
            default:
                type = 4;
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //Debug.Log("i" + i + "j" + j + inputMatrix[i, j]);
                inputMatrix[i, j] = false;
            }
        }
    }

    bool isHitsFull()
    {
        for (int i = 0; i < 5; i++)
        {
            if (hits[i] == null)
            {
                hitsFull = false;
                return false;
            }
        }
        hitsFull = true;
        return true;
    }

    void Initialize()
    {
        hits[0] = null;
        hits[1] = null;
        hits[2] = null;
        hits[3] = null;
        hits[4] = null;
        for (int i = 0; i < 4; i++)
        {
            colors[i] = "none";
        }
    }
}
