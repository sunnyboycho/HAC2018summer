using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSubscript : MonoBehaviour {

    [SerializeField]
    Sprite[] marbles = new Sprite[3];

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

            }
            else if (input[i].Equals("green"))
            {

            }
            else if (input[i].Equals("blue"))
            {

            }
            else
            {
                Debug.Log("Stat allocation error no color input");
            }
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = marbles[0];
        }
    }
}
