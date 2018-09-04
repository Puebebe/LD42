using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    bool isGrabbed = false;
    GameObject handle;
    public float distance = 22f;
    float minDistance = 5f;
    float maxDistance = 30f;
    int multiplier = 1;

    int sensitivity = 10;
    float currentSensitivity;

    float horizontalSpeed = 4f;
    float verticalSpeed = 4f;
    float rotationDelay = 0.2f;

    Rigidbody rb;

    void Start()
    {
        handle = new GameObject("Handle");
        handle.transform.SetParent(transform);
        handle.transform.position = transform.position;
        Cursor.lockState = CursorLockMode.Confined;
        currentSensitivity = sensitivity;
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDrag()
    {
        isGrabbed = true;
        Game.Objectives["Grab"] = true;

        if (Input.GetMouseButton(1))
        {
            //Cursor.lockState = CursorLockMode.Locked;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
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
                        transform.RotateAround(handle.transform.position, verticalAxis, 90 * Mathf.Sign(vInput));

                        rotationDelay = 0.2f;
                    }
                    if (hInput > 0.5 || hInput < -0.5)
                    {
                        transform.RotateAround(handle.transform.position, horizontalAxis, 90 * Mathf.Sign(hInput));

                        rotationDelay = 0.2f;
                    }
                }
            }
            else
            {
                transform.RotateAround(handle.transform.position, verticalAxis, v);
                transform.RotateAround(handle.transform.position, horizontalAxis, h);
            }
        }
        else
        {
            rb.useGravity = true;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);

            rb.velocity = (destination - handle.transform.position) * currentSensitivity;
        }
    }

    void OnMouseDown()
    {
        rb.isKinematic = false;

        Transform grabbedCube = FindGrabbedCube();
        handle.transform.SetPositionAndRotation(grabbedCube.position, grabbedCube.rotation);
    }

    Transform FindGrabbedCube()
    {
        Transform grabbedCube = transform;
        Vector3 nearestCube = transform.position;
        var cubes = GetComponentInChildren<Transform>();

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);

        foreach (Transform cube in cubes)
        {
            if (Vector3.Distance(cube.position, destination) <= Vector3.Distance(nearestCube, destination))
            {
                nearestCube = cube.position;
                grabbedCube = cube;
            }
        }

        return grabbedCube;
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
            {
                multiplier = 1;
                rb.angularVelocity = Vector3.zero;
                transform.rotation = transform.rotation.AlignToRightAngles();
            }
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

            if (Input.GetButton("Jump") || Input.GetMouseButton(2))
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
