using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    float maxDistance = 40;
    float minDistance = 5;

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

        Vector3 newCameraPosition = Vector3.MoveTowards(Camera.main.transform.position, transform.position, Wheel);
        float newDistance = Vector3.Distance(newCameraPosition, transform.position);

        if (newDistance < maxDistance && newDistance > minDistance)
        {
            float distanceDifference = newDistance - Vector3.Distance(Camera.main.transform.position, transform.position);
            Camera.main.transform.position = newCameraPosition;

            var shapes = GameObject.Find("Shapes").GetComponentsInChildren<MouseDrag>();

            foreach (var shape in shapes)
            {
                if (shape.distance + distanceDifference > minDistance)
                    shape.distance += distanceDifference;  
            }
        }
    }
}
