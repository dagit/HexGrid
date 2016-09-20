using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    [SerializeField]

    float rotationX = 0f;
    float rotationY = 0f;

    [SerializeField]
    float sensitivityX = 500f;
    [SerializeField]
    float sensitivityY = 500f;
    [SerializeField]
    float walkSpeed = 10f;

    float clampAngle = 90.0f;

    bool mouseLock = false;

    void Start()
    {
        transform.position = Camera.main.transform.position;
    }

    // Update is called once per frame
    void LateUpdate() {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Comma))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * walkSpeed;
            //Debug.Log("walk forward: " + pos);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * walkSpeed;
            //Debug.Log("strafe left: " + pos);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.E))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * walkSpeed;
            //Debug.Log("strafe right: " + pos);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.O))
        {
            transform.position += transform.TransformDirection(Vector3.back) * Time.deltaTime * walkSpeed;
            //Debug.Log("walk backwards: " + pos);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mouseLock = !mouseLock;
        }

        if (!mouseLock)
        {

            rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

            rotationY = Mathf.Clamp(rotationY, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(-rotationY, rotationX, 0.0f);
            transform.rotation = localRotation;
        }

        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = transform.rotation;
	}
}
