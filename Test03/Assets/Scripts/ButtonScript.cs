using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    [SerializeField]
    GameObject[] portraits;

    [SerializeField]
    GameObject pointA;

    [SerializeField]
    GameObject pointB;

    bool pointAOccupied = false;

    bool pointBOccupied = false;

    public bool PointAOccupied
    {
        get
        {
            return pointAOccupied;
        }
    }

    public bool PointBOccupied
    {
        get
        {
            return pointBOccupied;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            portraitDestroy();
            pointAOccupied = false;
            pointBOccupied = false;
        }
    }

    public void onButtonClick(int i)
    {
        if (!pointAOccupied)
        {
            pointAOccupied = true;
            Vector3 position = new Vector3(pointA.transform.position.x, pointA.transform.position.y, pointA.transform.position.z);
            GameObject newPortrait = Instantiate(portraits[i], position, Quaternion.identity);
            newPortrait.transform.SetParent(pointA.transform);
        }
        else if (!pointBOccupied)
        {
            pointBOccupied = true;
            Vector3 position = new Vector3(pointB.transform.position.x, pointB.transform.position.y, pointB.transform.position.z);
            GameObject newPortrait = Instantiate(portraits[i], position, Quaternion.identity);
            newPortrait.transform.SetParent(pointB.transform);
        }
        else
        {
            Debug.Log("ConversationSlotsFull");
        }
        
    }

    void portraitDestroy()
    {
        Destroy(pointA.transform.GetChild(0).gameObject);
        Destroy(pointB.transform.GetChild(0).gameObject);
    }
}
