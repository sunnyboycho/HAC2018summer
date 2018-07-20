using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour {

    [SerializeField]
    bool fixCamera = true;

    [SerializeField]
    Camera referenceCamera;

    [SerializeField]
    Transform[] positions = new Transform[3];

    Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        float x = referenceCamera.WorldToScreenPoint(positions[0].position).x;
        float y = referenceCamera.WorldToScreenPoint(positions[0].position).y;
        float width = referenceCamera.WorldToScreenPoint(positions[1].position).x - referenceCamera.WorldToScreenPoint(positions[0].position).x;
        float height = referenceCamera.WorldToScreenPoint(positions[1].position).y - referenceCamera.WorldToScreenPoint(positions[0].position).y;
        camera.pixelRect = new Rect(x, y, width, height);
    }
	
	// Update is called once per frame
	void Update () {
        if (fixCamera)
        {
            float x = referenceCamera.WorldToScreenPoint(positions[0].position).x;
            float y = referenceCamera.WorldToScreenPoint(positions[0].position).y;
            float width = referenceCamera.WorldToScreenPoint(positions[1].position).x - referenceCamera.WorldToScreenPoint(positions[0].position).x;
            float height = referenceCamera.WorldToScreenPoint(positions[1].position).y - referenceCamera.WorldToScreenPoint(positions[0].position).y;
            camera.pixelRect = new Rect(x, y, width, height);
        }
    }
}
