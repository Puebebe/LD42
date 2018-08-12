using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {


	Vector3 startPosition;
	Quaternion startRotation;

	// Use this for initialization
	void Start () {

		startPosition = Camera.main.transform.position;
		startRotation = Camera.main.transform.rotation;

	}

	// Update is called once per frame
	void Update () {







		if(Input.GetKey(KeyCode.A)){
			transform.Rotate( 0, 1, 0);
		}


		if(Input.GetKey(KeyCode.D)){
			transform.Rotate( 0, -1, 0);

		}

		float Wheel = 0;

		if(!Input.GetMouseButton(0)){
	 		Wheel = Input.GetAxis("Mouse ScrollWheel") * 50;
		}


	Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position, Wheel);

	if(Vector3.Distance(Camera.main.transform.position, transform.position) > 40 )
	Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position, 3);


	if(Vector3.Distance(Camera.main.transform.position, transform.position) == 0 ){
		Camera.main.transform.position = startPosition;
		Camera.main.transform.rotation = startRotation;
	}


	}
}
