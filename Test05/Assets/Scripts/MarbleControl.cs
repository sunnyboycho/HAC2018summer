using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleControl : MonoBehaviour {
    
    int marbleSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(-marbleSpeed, 0f);
	}
}
