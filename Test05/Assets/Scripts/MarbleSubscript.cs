using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSubscript : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveColors(string[] input)
    {
        for (int i = 0; i < 4; i++)
        {
            if (input[i].Equals("red"))
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (input[i].Equals("green"))
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (input[i].Equals("blue"))
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                Debug.Log("Stat allocation error no color input");
            }
        }
    }
}
