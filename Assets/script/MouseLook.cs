using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    public Transform playerBody;
    float xRotation = 0f;
    //  public GameObject Camera;
   [SerializeField] private Camera Camera;

    public PhotonView cameraview;
    // Start is called before the first frame update

    private void Start()
    {           
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!cameraview.IsMine)
        {
           // Destroy(Camera);
           Camera.enabled = false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraview.IsMine)
        {

            if (Input.GetKeyDown("1"))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            if (Input.GetKeyDown("2"))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);


        }
    }
}
