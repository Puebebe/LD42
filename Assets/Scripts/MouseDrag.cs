using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

	float distance = 10;

	[SerializeField]
	[Range(1,20)]
	int sensitivity = 10;


	void Start(){
		Cursor.lockState = CursorLockMode.Confined;
		// POWYŻSZA LINIJKA MOŻE BYĆ BUGGOGENNA!!!!!!
	}

	void OnMouseDrag(){

		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

		Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);



		GetComponent<Rigidbody>().velocity = (destination - transform.position) * sensitivity;

	}






}
