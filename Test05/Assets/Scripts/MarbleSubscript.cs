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

    public void ReceiveColors(int[] input)
    {
        for (int i = 0; i < 4; i++)
        {
            switch (input[i])
            {
                case 0:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 1:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                case 2:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 3:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f);
                    break;
                case 4:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.5f, 1, 0.5f);
                    break;
                case 5:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1);
                    break;
            }
        }
    }
}
