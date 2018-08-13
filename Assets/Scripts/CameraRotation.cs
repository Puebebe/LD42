using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    float maxDistance = 40;
    float minDistance = 5;
    //Vector3 startPosition;
    //Quaternion startRotation;

    // Use this for initialization
    void Start()
    {
        //startPosition = Camera.main.transform.position;
        //startRotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, -1, 0);
        }

        float Wheel = 0;

        if (!Input.GetMouseButton(0))
        {
            Wheel = Input.GetAxis("Mouse ScrollWheel") * 50;
        }


        //Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position, Wheel);
        Vector3 newCameraPosition = Vector3.MoveTowards(Camera.main.transform.position, transform.position, Wheel);

        if (Vector3.Distance(newCameraPosition, transform.position) < maxDistance && Vector3.Distance(newCameraPosition, transform.position) > minDistance)
            Camera.main.transform.position = newCameraPosition;

        /*
        if (Vector3.Distance(Camera.main.transform.position, transform.position) > 40)
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position, 3f);

        if (Vector3.Distance(Camera.main.transform.position, transform.position) < 5)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, Camera.main.transform.forward, -0.1f);
            //Camera.main.transform.position = startPosition;
            //Camera.main.transform.rotation = startRotation;
        }
        */
    }
}
