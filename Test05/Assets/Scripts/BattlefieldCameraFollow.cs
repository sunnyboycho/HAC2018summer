using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldCameraFollow : MonoBehaviour {

    public bool bDragging = true;
    public float panSpeed = 5;
    private Vector3 oldPos;
    private Vector3 panOrigin;

    [SerializeField]
    float lerpSpeed;

    [SerializeField]
    float xMin;

    [SerializeField]
    float xMax;

    [SerializeField]
    float yMin;

    [SerializeField]
    float yMax;

    [SerializeField]
    Transform playerBase;

    [SerializeField]
    GameObject playerUnits;

    Transform targetUnit;

    bool unitPresent = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            bDragging = true;
            oldPos = transform.position;
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);                    //Get the ScreenVector the mouse clicked
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;    //Get the difference between where the mouse clicked and where it moved
            transform.position = oldPos + -pos * panSpeed;                                         //Move the position of the camera to simulate a drag, speed * 10 for screen to worldspace conversion
        }

        if (Input.GetMouseButtonUp(0))
        {
            bDragging = false;

            if (!unitPresent)
            {
                CenterOnBase();
            }
            else
            {
                CenterOnUnit();
            }
        }*/
        
        if (!unitPresent)
        {
            CenterOnBase();
        }
        else
        {
            CenterOnUnit();
        }
	}

    void CenterOnBase()
    {
        Vector3 target = new Vector3(Mathf.Clamp(playerBase.position.x, xMin, xMax), Mathf.Clamp(playerBase.position.y, yMin, yMax), -10);
        transform.position = Vector3.Lerp(transform.position, target, lerpSpeed * Time.deltaTime);
        if (playerUnits.transform.childCount > 0)
        {
            unitPresent = true;
        }
    }

    void CenterOnUnit()
    {
        if (playerUnits.transform.childCount == 0)
        {
            unitPresent = false;
        }
        else
        {
            for (int i = 0; i < playerUnits.transform.childCount; i++)
            {
                Transform temp = playerUnits.transform.GetChild(i).transform;
                if (targetUnit == null)
                {
                    targetUnit = temp;
                }
                else
                {
                    if (targetUnit.position.x < temp.position.x)
                    {
                        targetUnit = temp;
                    }
                }
            }
            Vector3 target = new Vector3(Mathf.Clamp(targetUnit.position.x, xMin, xMax), Mathf.Clamp(targetUnit.position.y, yMin, yMax), -10);
            transform.position = Vector3.Lerp(transform.position, target, lerpSpeed * Time.deltaTime);
        }
    }
}
