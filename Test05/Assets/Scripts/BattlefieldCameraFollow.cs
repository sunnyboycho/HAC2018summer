using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldCameraFollow : MonoBehaviour {

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
        gameObject.transform.position = new Vector3(Mathf.Clamp(playerBase.position.x, xMin, xMax), Mathf.Clamp(playerBase.position.y, yMin, yMax), -10);
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
            gameObject.transform.position = new Vector3(Mathf.Clamp(targetUnit.position.x, xMin, xMax), Mathf.Clamp(targetUnit.position.y, yMin, yMax), -10);
        }
    }
}
