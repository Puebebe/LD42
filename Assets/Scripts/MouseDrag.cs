using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

	float distance = 10;

	[SerializeField]
	[Range(1,20)]
	int sensitivity = 10;

    float horizontalSpeed = 2f;
    float verticalSpeed = 2f;
    Rigidbody rb;

    void Start()
    {
		Cursor.lockState = CursorLockMode.Confined;
        rb = GetComponent<Rigidbody>();
    }

	void OnMouseDrag()
    {
        if (Input.GetMouseButton(1))
        {
            //Cursor.lockState = CursorLockMode.Locked;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(v, h, 0);
        }
        else
        {
            rb.useGravity = true;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);

            rb.velocity = (destination - transform.position) * sensitivity;
        }
	}

    void OnMouseUp()
    {
        rb.useGravity = true;
        rb.freezeRotation = false;
        //Cursor.lockState = CursorLockMode.None;
    }
}
