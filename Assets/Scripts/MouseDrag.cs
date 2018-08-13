using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{

    float distance = 10f;
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

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                //transform.rotation = Quaternion.identity;
                //Vector3 newRotation = new Vector3(Mathf.Round(transform.eulerAngles.x / 90) * 90, Mathf.Round(transform.eulerAngles.y / 90) * 90, Mathf.Round(transform.eulerAngles.z / 90) * 90);
                //transform.rotation = Quaternion.Euler(newRotation);
                //transform.rotation = transform.rotation.AlignToRightAngles();
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                rotationDelay -= Time.deltaTime;

                if (rotationDelay <= 0)
                {
                    transform.rotation = transform.rotation.AlignToRightAngles();

                    if (vInput > 0.5 || vInput < -0.5)
                    {
                        transform.Rotate(Vector3.right, 90 * Mathf.Sign(vInput));

                        rotationDelay = 0.2f;
                    }
                    if (hInput > 0.5 || hInput < -0.5)
                    {
                        transform.Rotate(Vector3.up, 90 * Mathf.Sign(hInput));

                        rotationDelay = 0.2f;
                    }
                }
                
                //Debug.Log("vInput=" + vInput + " hInput=" + hInput);
                //transform.localRotation = Quaternion.Euler(v, h, 0);
                //transform.rotation = Quaternion.identity;
                //transform.Rotate(v, h, 0, Space.Self);
                /*
                if (vInput > 0.1 || hInput > 0.1)
                {
                    if (v > h)
                    {
                        v += 90 * Mathf.Sign(v);
                        v = (int)(v / 90);
                        v *= 90;
                        h = 0;
                    }
                    else
                    {
                        h += 90 * Mathf.Sign(h);
                        h = (int)(h / 90);
                        h *= 90;
                        v = 0;
                    }

                    Debug.Log("v=" + v + "h=" + h);
                    //transform.localRotation = Quaternion.Euler(v, h, 0);
                    transform.rotation = Quaternion.identity;
                    transform.Rotate(v, h, 0, Space.Self);
                    
                }
                */
            }
            else
                //transform.Rotate(v, h, 0);
                //transform.RotateAroundLocal(Vector3.up, 200 * Time.deltaTime);
                //transform.RotateAround(transform.position, Vector3.up, 200 * Time.deltaTime);
                transform.Rotate(new Vector3(v, h, 0), Space.World);
        }
        else
        {
            rb.useGravity = true;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);

            rb.velocity = (destination - transform.position) * currentSensitivity;
        }
    }

    void OnMouseUp()
    {
        rb.useGravity = true;
        rb.freezeRotation = false;
        currentSensitivity = sensitivity;
        //StartCoroutine("SensitivityLerp");
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
        if (Input.GetKey(KeyCode.LeftControl))
            multiplier = 1;
        else
            multiplier = 5;

        distance += Input.GetAxis("Mouse ScrollWheel") * multiplier;

        if (Input.GetMouseButtonUp(1))
        {
            currentSensitivity = 1;
            StartCoroutine("SensitivityLerp");
        }
    }
}
