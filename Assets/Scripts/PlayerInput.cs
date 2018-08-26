using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    bool isGrabbed = false;
    Vector3 grabOffset = Vector3.zero;
    public float distance = 22f;
    float minDistance = 5f;
    float maxDistance = 30f;
    int multiplier = 1;

    int sensitivity = 10;
    float currentSensitivity;

    float horizontalSpeed = 2f;
    float verticalSpeed = 2f;
    float rotationDelay = 0.2f;

    Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentSensitivity = sensitivity;
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDrag()
    {
        isGrabbed = true;

        if (Input.GetMouseButton(1))
        {
            //Cursor.lockState = CursorLockMode.Locked;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            float hInput = Input.GetAxis("Mouse X");
            float vInput = Input.GetAxis("Mouse Y");
            float h = horizontalSpeed * hInput;
            float v = verticalSpeed * vInput;
            Vector3 horizontalAxis = Vector3.zero;
            Vector3 verticalAxis = Vector3.zero;
            Vector3 cameraPos = Camera.main.transform.position;
            float x = cameraPos.x;
            float z = cameraPos.z;

            if (x > z && x + z < 0)
            {
                horizontalAxis = Vector3.back;
                verticalAxis = Vector3.right;
            }
            else if (x < z && x + z < 0)
            {
                horizontalAxis = Vector3.left;
                verticalAxis = Vector3.back;
            }
            else if (x < z && x + z > 0)
            {
                horizontalAxis = Vector3.forward;
                verticalAxis = Vector3.left;
            }
            else if (x > z && x + z > 0)
            {
                horizontalAxis = Vector3.right;
                verticalAxis = Vector3.forward;
            }


            if (Input.GetKey(KeyCode.LeftControl))
            {
                rotationDelay -= Time.deltaTime;

                if (rotationDelay <= 0)
                {
                    transform.rotation = transform.rotation.AlignToRightAngles();

                    if (vInput > 0.5 || vInput < -0.5)
                    {
                        transform.Rotate(verticalAxis, 90 * Mathf.Sign(vInput), Space.World);

                        rotationDelay = 0.2f;
                    }
                    if (hInput > 0.5 || hInput < -0.5)
                    {
                        transform.Rotate(horizontalAxis, 90 * Mathf.Sign(hInput), Space.World);

                        rotationDelay = 0.2f;
                    }
                }
            }
            else
            {
                transform.Rotate(verticalAxis, v, Space.World);
                transform.Rotate(horizontalAxis, h, Space.World);
            }
        }
        else
        {
            rb.useGravity = true;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);

            if (!Parameters.parameterRotation)
                rb.velocity = (destination - grabOffset - transform.position) * currentSensitivity;
            else
                rb.velocity = (destination - transform.position) * currentSensitivity;
        }
    }

    void OnMouseDown()
    {
        rb.isKinematic = false;

        grabOffset = FindGrabOffset();
    }

    Vector3 FindGrabOffset()
    {
        Vector3 nearestCube = transform.position;
        Vector3 searchedOffset = Vector3.zero;
        var cubes = GetComponentInChildren<Transform>();

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);

        Debug.Log("destination" + destination);

        foreach (Transform cube in cubes)
        {
            Debug.Log("cube.localPosition" + cube.localPosition);
            Debug.Log("cube.position" + cube.position);
            Debug.Log("Distance(cube.position, destination) = " + Vector3.Distance(cube.position, destination));
            Debug.Log("Distance(nearestCube, destination) = " + Vector3.Distance(nearestCube, destination));
            if (Vector3.Distance(cube.position, destination) < Vector3.Distance(nearestCube, destination))
            {
                nearestCube = cube.position;
                searchedOffset = cube.localPosition;
            }
        }

        Debug.Log("nearestCube = " + nearestCube + ", transform.position = " + transform.position);
        Debug.Log("Searched offset = " + searchedOffset);

        return searchedOffset;
    }

    void OnMouseUp()
    {
        isGrabbed = false;
        rb.useGravity = true;
        //rb.freezeRotation = false;
        rb.freezeRotation = !Parameters.parameterRotation;
        currentSensitivity = sensitivity;
        //Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator SensitivityLerp()
    {
        float t = 0;

        while (currentSensitivity < sensitivity - 1)
        {
            //Debug.Log(currentSensitivity);
            currentSensitivity = Mathf.Lerp(1, sensitivity, t);
            t += 0.5f * Time.deltaTime;
            yield return null;
        }

        currentSensitivity = sensitivity;
    }

    void Update()
    {
        if (isGrabbed)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                multiplier = 1;
            else
                multiplier = 5;

            float distanceChange = Input.GetAxis("Mouse ScrollWheel") * multiplier;
            float newDistance = distance + distanceChange;

            if ((distanceChange < 0 && newDistance > minDistance) || (distanceChange > 0 && newDistance < maxDistance))
            {
                distance += distanceChange;
                //Debug.Log(distance);
            }

            if (Input.GetMouseButtonUp(1))
            {
                currentSensitivity = 1;
                StartCoroutine("SensitivityLerp");
            }

            if (Input.GetMouseButton(2) || Input.GetButton("Jump"))
            {
                rb.isKinematic = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
            Game.ShowHelpScreen(true);

        if (Input.GetKeyUp(KeyCode.Tab))
            Game.ShowHelpScreen(false);
    }
}
