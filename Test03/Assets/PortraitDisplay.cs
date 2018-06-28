using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitDisplay : MonoBehaviour {

    public Portrait portrait;

	// Use this for initialization
	void Start () {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = portrait.face;
    }
}
