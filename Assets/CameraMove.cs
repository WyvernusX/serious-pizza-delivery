using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject cameraItem;     
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
      float mouseX = Input.GetAxis("Mouse X");
      float mouseY = Input.GetAxis("Mouse Y");
      float mouseSensitivity = 2000f;
 
      gameObject.transform.Rotate(Vector3.up * (mouseX * mouseSensitivity) * Time.deltaTime);
      cameraItem.transform.Rotate(Vector3.right * (-mouseY * mouseSensitivity) * Time.deltaTime); 
    }

    void FixedUpdate() {
       
    }
}
